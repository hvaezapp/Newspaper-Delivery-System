using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class PaymentViewModel
    {
        public string Description { get; set; }


        public string RefCode { get; set; }


        [Required]
        //[Display(Name = "Order")]
        public string OrderCode { get; set; }


        [Required]
        [Display(Name = "Customer")]
        public long CustomerId { get; set; }



        public long OrderId { get; set; }



        [Required]
        public long Price { get; set; }
    }
}
