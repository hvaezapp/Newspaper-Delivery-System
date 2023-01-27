using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class Order : BaseEntity<long>
    {
        [Display(Name = "Customer")]
        public long FkCustomerId { get; set; }


        [Display(Name = "TotalPrice")]
        public long TotalPrice { get; set; }


        public string  Code { get; set; }


        public bool IsComplete { get; set; }

        public DateTime CompleteDateTime { get; set; }



        #region relations

        [ForeignKey(nameof(FkCustomerId))]
        public virtual Customer Tbl_Customer { get; set; }

        #endregion
    }
}
