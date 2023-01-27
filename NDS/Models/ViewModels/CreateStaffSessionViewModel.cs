using NDS.Models.Domain;
using System.Collections.Generic;

namespace NDS.Models.ViewModels
{
    public class CreateStaffSessionViewModel
    {
        public IEnumerable<NameViewModel> staffs { get; set; }

        public StaffSessionReady staffSession { get; set; }


        public CreateStaffSessionViewModel()
        {
            staffs = new List<NameViewModel>();
        }
    }
}
