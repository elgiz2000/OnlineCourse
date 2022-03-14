using OnlineCourse.Entities.Dtos.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Entities.Dtos.Student
{
    public class StudentGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<CourseListDto>? Courses { get; set; }
    }
}
