using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IGadgetCatalogRepository _gadgetCatalogRepository;
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IGadgetCatalogRepository gadgetCatalogRepository, IOrderProcessor orderProcessor)
        {
            _gadgetCatalogRepository = gadgetCatalogRepository;
            _orderProcessor = orderProcessor;
        }

        /// <summary>
        /// Add gadget to cart and redirect user.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects user to cart index page.</returns>
        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            GadgetCatalog gadget = _gadgetCatalogRepository.GadgetCatalogs.FirstOrDefault(p => p.Id == Id);
            if (gadget != null)
            {
                cart.AddItem(gadget, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// Remove gadget from cart and redirect user.
        /// </summary>
        /// <param name="gadgetId"></param>
        /// <param name="returnUrl"></param>
        /// <returns>Redirects user to cart index page.</returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int gadgetId, string returnUrl)
        {
            GadgetCatalog gadget = _gadgetCatalogRepository.GadgetCatalogs.FirstOrDefault(p => p.Id == gadgetId);
            if (gadget != null)
            {
                cart.RemoveLine(gadget);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                cart = cart,
                returnUrl = returnUrl
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "No Items in cart!");
            }
            if (ModelState.IsValid)
            {
               _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}