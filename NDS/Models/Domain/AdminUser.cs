using System;
using System.ComponentModel.DataAnnotations;

namespace NDS.Models.Domain
{
    public class AdminUser : BaseEntity<long>
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public string Phone { get; set; }

        public string ImageUrl { get; set; }


        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }
}
