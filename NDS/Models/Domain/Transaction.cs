using System.ComponentModel.DataAnnotations;

namespace NDS.Models.Domain
{
    public class Transaction : BaseEntity<long>
    {


        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }


        [Required]
        public string Price { get; set; }


        [Display(Name = "TransactionType")]
        [Required]
        public int Type { get; set; }


    }
}
