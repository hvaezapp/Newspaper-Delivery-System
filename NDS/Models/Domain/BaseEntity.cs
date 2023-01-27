using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDS.Models.Domain
{
    public class BaseEntity<T> where T : struct
    {
        public T Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }


        public BaseEntity()
        {
            IsDeleted = false;
            CreateDate = DateTime.Now;
        }
    }
}
