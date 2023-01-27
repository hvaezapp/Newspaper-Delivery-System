using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDS.Models.Domain
{
    public class StaffSessionReady : BaseEntity<long>
    {

        [Display(Name = "Staff")]
        public long FkStaffId { get; set; }


        [Display(Name = "SessionDate")]
        [DataType(DataType.Date)]
        public DateTime SessionDateTime { get; set; }



        [Display(Name = "ReadyState")]
        public int SessionReadyStatus { get; set; }



        [ForeignKey(nameof(FkStaffId))]
        public virtual Staff Tbl_Staff { get; set; }
    }
}
