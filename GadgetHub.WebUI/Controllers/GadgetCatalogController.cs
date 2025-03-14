using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetCatalogController : Controller
    {
        private readonly IGadgetCatalogRepository gadgetRepo;

        public GadgetCatalogController(IGadgetCatalogRepository repo)
        {
            gadgetRepo = repo;
        }

        public int PageSize = 6;
        public ViewResult List(string category, int page = 1)
        {
            GadgetListViewModel model = new GadgetListViewModel
            {
                Catalog = gadgetRepo.GadgetCatalogs
                    .Where(g => category == null || g.Category == category)
                    .OrderBy(g => g.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                        gadgetRepo.GadgetCatalogs.Count() :
                        gadgetRepo.GadgetCatalogs.Where(g => g.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

    }
}