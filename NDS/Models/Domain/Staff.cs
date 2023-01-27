using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.Domain
{
    public class Staff : BaseEntity<long>
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public string NationalCode { get; set; }

        [Required]
        public string Address { get; set; }


        [Display(Name = "UserImage")]
        public string ImageUrl { get; set; }
    }
}
