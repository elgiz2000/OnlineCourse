using OnlineCourse.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class Department : BaseEntity
    {
        public List<Course>? Courses { get; set; }
    }
}
