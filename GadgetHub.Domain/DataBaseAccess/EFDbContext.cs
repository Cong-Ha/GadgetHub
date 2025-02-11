using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.DataBaseAccess
{
    public class EFDbContext : DbContext
    {
        public DbSet<GadgetCatalog> GadgetCatalogs { get; set; }
    }
}
