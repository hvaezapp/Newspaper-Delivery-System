using NDS.Models.Domain;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class SupplierViewModel
    {

        public Supplier supplier { get; set; }

        public IEnumerable<Province> provinces { get; set; }

        public IEnumerable<City> cities { get; set; }



        public SupplierViewModel()
        {
            provinces = new List<Province>();
            cities = new List<City>();
        }
    }
}
