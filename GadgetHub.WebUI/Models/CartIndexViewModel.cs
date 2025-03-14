using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart cart { get; set; }
        public string returnUrl { get; set; }
    }
}