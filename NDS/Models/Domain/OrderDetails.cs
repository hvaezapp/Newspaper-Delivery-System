using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class OrderDetails : BaseEntity<long>
    {

        [Display(Name = "Order")]

        public long FkOrderId { get; set; }



        [Display(Name = "Product")]
        [Required]
        public long FkProductId { get; set; }




        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }




        #region relations

        [ForeignKey(nameof(FkOrderId))]
        public virtual Order Tbl_Order { get; set; }


        [ForeignKey(nameof(FkProductId))]
        public virtual Product Tbl_Product { get; set; }

        #endregion
    }
}
