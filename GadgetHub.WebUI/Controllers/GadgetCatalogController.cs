using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetCatalogController : Controller
    {
        private readonly IGadgetCatalogRepository gadgetRepo;

        public GadgetCatalogController(IGadgetCatalogRepository repo)
        {
            gadgetRepo = repo;
        }

        // GET: GadgetCatalog
        public ViewResult List()
        {
            return View(gadgetRepo.GadgetCatalogs);
        }
    }
}