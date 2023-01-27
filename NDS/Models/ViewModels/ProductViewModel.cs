using NDS.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Supplier> suppliers { get; set; }
        public Product product { get; set; }


        public ProductViewModel()
        {
            suppliers = new List<Supplier> ();
        }
    }
}
