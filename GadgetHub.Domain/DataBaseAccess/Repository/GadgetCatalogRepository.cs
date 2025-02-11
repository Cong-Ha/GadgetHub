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
    }
}
