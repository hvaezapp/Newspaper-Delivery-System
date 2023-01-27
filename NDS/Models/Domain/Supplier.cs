using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class Supplier : BaseEntity<long>
    {
        [Required]
        public string Title { get; set; }


        [Required]
        public string CompanyName { get; set; }


        [Required]
        public string Address { get; set; }



        [Required]
        public string Tel { get; set; }

 


        [Required]
        [Display(Name = "City")]

        public int FkCityId { get; set; }




        [Required]
        [Display(Name = "Province")]
        public int FkProvinceId { get; set; }




        #region Relations

        [ForeignKey(nameof(FkCityId))]
        public virtual City Tbl_City { get; set; }

        [ForeignKey(nameof(FkProvinceId))]
        public virtual Province Tbl_Province { get; set; }

        #endregion
    }
}
