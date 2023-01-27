using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class Province : BaseEntity<int>
    {

        [Required]
        public string Title { get; set; }

      
        
    }
}
