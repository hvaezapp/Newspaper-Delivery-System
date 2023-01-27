using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class PublishPlan : BaseEntity<long>
    {

    
        public string Description { get; set; }


        [Required]
        [Display(Name = "Customer")]
        public long FkCustomerId { get; set; }




        [Required]
        [Display(Name = "Staff(distributary)")]
        public long FkStaffId { get; set; }




        [Required]
        [Display(Name = "Product")]
        public long FkProductId { get; set; }




        [Required]
        public int Quantity { get; set; }




        [Display(Name = "DeliveryDate")]
        [Required]
        [DataType(DataType.Date)]
      
        public DateTime DeliveryDateTime { get; set; }



        public int Status { get; set; }



        #region relations

        [ForeignKey(nameof(FkCustomerId))]
        public virtual Customer Tbl_Customer { get; set; }



        [ForeignKey(nameof(FkStaffId))]
        public virtual Staff Tbl_Staff { get; set; }


        [ForeignKey(nameof(FkProductId))]
        public virtual Product Tbl_Product { get; set; }


        #endregion
    }
}
