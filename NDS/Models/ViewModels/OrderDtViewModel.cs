using NDS.Models.Domain;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class OrderDtViewModel
    {
        public IEnumerable<OrderDetails> orderDetails { get; set; }
        public Order order { get; set; }


        public OrderDtViewModel()
        {
            orderDetails = new List<OrderDetails>();
        }
    }
}
