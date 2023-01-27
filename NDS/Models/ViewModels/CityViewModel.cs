using NDS.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class CityViewModel
    {
        public IEnumerable<Province> provinces { get; set; }

        public City city { get; set; }


        public CityViewModel()
        {
            provinces = new List<Province>();

        }
    }
}
