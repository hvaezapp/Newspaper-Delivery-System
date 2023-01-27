using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class Customer : BaseEntity<long>
    {

      

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Mobile { get; set; }


        [Required]
        public int Gender { get; set; }


        [Display(Name = "BirthDate")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }






        // address
        [Required]
        public string Address { get; set; }


        public double latitude { get; set; }

        public double longitude { get; set; }



        [Required]
        [Display(Name = "City")]
        
        public int FkCityId { get; set; }


        [Required]
        [Display(Name = "Province")]
        public int FkProvinceId { get; set; }

       


        [ForeignKey(nameof(FkCityId))]
        public virtual City Tbl_City { get; set; }





        [ForeignKey(nameof(FkProvinceId))]
        public virtual Province Tbl_Province { get; set; }
    }
}
