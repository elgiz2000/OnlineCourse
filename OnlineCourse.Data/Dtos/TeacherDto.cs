using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Entities.Dtos
{
    public class TeacherDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public int Salary { get; set; }
    }
}
