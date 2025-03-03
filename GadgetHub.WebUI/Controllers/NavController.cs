using GadgetHub.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IGadgetCatalogRepository _catalogRepository;

        public NavController(IGadgetCatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _catalogRepository.GadgetCatalogs
                .Select(c => c.Category)
                .Distinct()
                .OrderBy(c => c);
            return PartialView(categories);
        }
    }
}