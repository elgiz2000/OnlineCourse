using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data
{
    public class Course 
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        public int Price { get; set; }
        public Teacher? Teacher { get; set; }
        public int TeacherId { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public List<Student>? Students { get; set; }
    }
}
