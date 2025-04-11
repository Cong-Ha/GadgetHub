using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Abstract
{
    public interface IGadgetCatalogRepository
    {
        IEnumerable<GadgetCatalog> GadgetCatalogs { get; }

        void SaveGadget(GadgetCatalog gadget);

        GadgetCatalog DeleteGadget(int gadgetId);
    }
}
