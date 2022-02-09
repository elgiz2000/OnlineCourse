using OnlineCourse.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class Course : BaseEntity
    {
        public int Price { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
