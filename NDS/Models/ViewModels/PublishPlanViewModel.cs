using NDS.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class PublishPlanViewModel
    {
        public IEnumerable<NameViewModel> staffs { get; set; }
        public IEnumerable<NameViewModel> customers { get; set; }

        public IEnumerable<Product> products { get; set; }
        public PublishPlan publishPlan { get; set; }
        public PublishPlanViewModel()
        {
            staffs = new List<NameViewModel>();
            customers = new List<NameViewModel>();
            products = new List<Product>();

        }
    }
}
