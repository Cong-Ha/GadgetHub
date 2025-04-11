using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGadgetCatalogRepository _gadgetCatalogRepo;

        public AdminController(IGadgetCatalogRepository gadgetCatalogRepo)
        {
            _gadgetCatalogRepo = gadgetCatalogRepo;
        }
        public ActionResult Index()
        {
            return View(_gadgetCatalogRepo.GadgetCatalogs);
        }

        public ActionResult Edit(int id)
        {
            GadgetCatalog gadget = _gadgetCatalogRepo.GadgetCatalogs
                .FirstOrDefault(g => g.Id == id);

            ViewBag.Title = "Edit " + gadget.Name;

            return View(gadget);
        }

        [HttpPost]
        public ActionResult Edit(GadgetCatalog gadget)
        {
            if (ModelState.IsValid)
            {
                _gadgetCatalogRepo.SaveGadget(gadget);
                TempData["message"] = string.Format("{0} has been saved", gadget.Name);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Title = "Add New Gadget";
                return View(gadget);
            }
        }

        public ViewResult Create()
        {
            ViewBag.Title = "Add New Gadget";
            return View("Edit", new GadgetCatalog());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            GadgetCatalog deletedGadget = _gadgetCatalogRepo.DeleteGadget(id);
            if (deletedGadget != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGadget.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(deletedGadget);
            }
        }
    }
}