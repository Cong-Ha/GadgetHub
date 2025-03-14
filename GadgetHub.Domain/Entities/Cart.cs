using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Entities
{
    public class CartLine
    {
        public GadgetCatalog Gadget { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(GadgetCatalog gadget, int quantity)
        {
            CartLine line = Lines
                .Where(g => g.Gadget.Id == gadget.Id)
                .FirstOrDefault();

            if(line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Gadget = gadget,
                    Quantity = quantity
                });
            } 
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(GadgetCatalog gadget)
        {
            lineCollection.RemoveAll(g => g.Gadget.Id == gadget.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(t => t.Gadget.Price * t.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
