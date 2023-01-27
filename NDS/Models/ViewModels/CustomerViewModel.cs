using NDS.Models.Domain;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class CustomerViewModel
    {

        public Customer customer { get; set; }

        public IEnumerable<Province> provinces { get; set; }

        public IEnumerable<City> cities { get; set; }



        public CustomerViewModel()
        {
            provinces = new List<Province>();
            cities = new List<City>();
        }
    }
}
