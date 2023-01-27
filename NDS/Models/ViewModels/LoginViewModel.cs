using NDS.Utility;
using System.ComponentModel.DataAnnotations;

namespace NDS.Models.ViewModels
{
    public class LoginViewModel
    {
        
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        
        public bool RememberMe { get; set; }
    }
}
