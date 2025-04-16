using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.DataBaseAccess.Repository
{
    public class GadgetCatalogRepository : IGadgetCatalogRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IEnumerable<GadgetCatalog> GadgetCatalogs
        {
            get { return _context.GadgetCatalogs; }
        }

        public void SaveGadget(GadgetCatalog gadget)
        {
            if (gadget.Id == 0)
            {
                _context.GadgetCatalogs.Add(gadget);
            }
            else
            {
                GadgetCatalog dbEntry = _context.GadgetCatalogs.Find(gadget.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = gadget.Name;
                    dbEntry.Description = gadget.Description;
                    dbEntry.Price = gadget.Price;
                    dbEntry.Category = gadget.Category;
                    dbEntry.Brand = gadget.Brand;
                    dbEntry.ImageData = gadget.ImageData;
                    dbEntry.ImageMimeType = gadget.ImageMimeType;
                }
            }
            _context.SaveChanges();
        }

        public GadgetCatalog DeleteGadget(int gadgetId)
        {
            GadgetCatalog dbEntry = _context.GadgetCatalogs.Find(gadgetId);
            if (dbEntry != null)
            {
                _context.GadgetCatalogs.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
