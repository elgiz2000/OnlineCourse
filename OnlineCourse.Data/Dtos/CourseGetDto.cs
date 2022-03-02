using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Entities.Dtos
{
    public class CourseGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Price { get; set; }
        public string? DepartmentName { get; set; }
    }
}
