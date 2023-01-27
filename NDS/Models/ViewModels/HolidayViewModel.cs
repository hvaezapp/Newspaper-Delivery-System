using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class HolidayViewModel
    {
        public string date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public bool @fixed { get; set; }
        public bool global { get; set; }
        public object counties { get; set; }
        public object launchYear { get; set; }
        public List<string> types { get; set; }
    }



}
