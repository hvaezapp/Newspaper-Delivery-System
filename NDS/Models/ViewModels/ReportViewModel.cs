using FMIS.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.ViewModels
{
    public class ReportViewModel
    {
        public int productCount { get; set; }

        public int customerCount { get; set; }

        public int supplierCount { get; set; }

        public int OrderCount { get; set; }


        public List<ChartViewModel> charts { get; set; }

        public ReportViewModel()
        {
            charts = new List<ChartViewModel>();
        }

    }
}
