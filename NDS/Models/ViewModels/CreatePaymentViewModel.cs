using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class CreatePaymentViewModel
    {
        public IEnumerable<NameViewModel> customers { get; set; }

        public PaymentViewModel payment { get; set; }

        public CreatePaymentViewModel()
        {
            customers = new List<NameViewModel>();
        }
    }
}
