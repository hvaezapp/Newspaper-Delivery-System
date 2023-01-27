using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.Domain
{
    public class Payment : BaseEntity<long>
    {

        public string Description { get; set; }


        public string RefCode { get; set; }


        [Required]
        [Display(Name = "Customer")]
        public long FkCustomerId { get; set; }




        [Required]
        [Display(Name = "Order")]
        public long FkOrderId { get; set; }



        [Required]
        public long Price { get; set; }




        #region Order

        [ForeignKey(nameof(FkCustomerId))]
        public virtual Customer Tbl_Customer { get; set; }


        [ForeignKey(nameof(FkOrderId))]
        public virtual Order Tbl_Order { get; set; }


        #endregion

    }
}
