using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class Product : BaseEntity<long>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        [Required]
        public int ProductType { get; set; }


        [Display(Name = "Supplier")]
        [Required]
        public long FkSupplierId { get; set; }


        [Required]
        public int Quantity { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public long Price { get; set; }



        [Required]
        [DataType(DataType.Date)]
        public DateTime DeliverDateTime { get; set; }


        public bool IsExist { get; set; }



        [ForeignKey(nameof(FkSupplierId))]
        public virtual Supplier Tbl_Supplier { get; set; }

    }
}
