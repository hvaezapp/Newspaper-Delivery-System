using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class City : BaseEntity<int>
    {

        [Display(Name = "Province")]
        public int FkProvinceId { get; set; }


        [Required]
        public string Title { get; set; }




        [ForeignKey(nameof(FkProvinceId))]
        public virtual Province Tbl_Province { get; set; }


    }
}
