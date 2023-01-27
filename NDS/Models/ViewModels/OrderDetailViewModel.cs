using NDS.Models.Domain;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public IEnumerable<Product> products { get; set; }

        public OrderDetails orderDetails { get; set; }

       

        public OrderDetailViewModel()
        {
            products = new List<Product>();
        }
    }
}
