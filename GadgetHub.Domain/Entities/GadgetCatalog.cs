using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GadgetHub.Domain.Entities
{
    [Table("GadgetCatalog")] //need this or migration will create a pluralized table
    public class GadgetCatalog
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a Gadget name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a Brand name")]
        public string Brand { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please specify a Category")]
        public string Category { get; set; }
    }
}
