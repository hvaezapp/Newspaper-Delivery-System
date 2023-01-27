using NDS.Models.Domain;
using NDS.Models.ViewModels;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class OrderViewModel
    {


        public IEnumerable<NameViewModel> customers { get; set; }

        public Order order { get; set; }

        public OrderViewModel()
        {

            customers = new List<NameViewModel>();
        }
    }
}
