using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class Teacher 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public int Salary { get; set; }

        public List<Course>? Courses { get; set; }


    }
}
