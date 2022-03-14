using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Entities.Dtos
{
    public class CourseDto
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public int TeacherId { get; set; }
        public int DepartmentId { get; set; }
    }
}
