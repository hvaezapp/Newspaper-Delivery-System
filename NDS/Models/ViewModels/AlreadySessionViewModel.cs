using NDS.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class AlreadySessionViewModel
    {
        public Staff staff { get; set; }

        public StaffSessionReady staffSession { get; set; }
    }
}
