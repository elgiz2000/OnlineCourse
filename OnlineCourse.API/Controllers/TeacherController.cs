using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;
using OnlineCourse.Entities.Dtos.Course;
using OnlineCourse.Entities.Dtos.Teacher;

namespace OnlineCourse.API.Controllers
{
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        private readonly TeacherRepository _teacherRepository = new();
        public TeacherController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            try
            {
                var teachers = _teacherRepository.GetTeachers();
                var response = teachers.Select(x => new TeacherGetDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    Salary = x.Salary,
                    Email=x.Email,
                    Courses = x?.Courses?.Select(x => new CourseListDto { Id = x.Id, Name = x.Name, Price = x.Price }).ToList(),
                }).ToList();
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            try
            {
                var teacher = _teacherRepository.GetTeacherById(id);
                var response = new TeacherGetDto
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Email = teacher.Email,
                    Salary = teacher.Salary,
                    Courses = teacher?.Courses?.Select(x => new CourseListDto { Id = x.Id, Name = x.Name, Price = x.Price }).ToList(),
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody]TeacherDto dto)
        {
            var teacher = new Teacher
            {
                Name = dto.Name,
                Email=dto.Email,
                Salary=dto.Salary,
            };
            try
            {
                _teacherRepository.AddTeacher(teacher);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            try
            {
                _teacherRepository.DeleteTeacher(id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher([FromBody] TeacherDto dto,int id)
        {
            var teacher = new Teacher
            {
                Id=id,
                Name=dto.Name,
                Email=dto.Email,
                Salary = dto.Salary,
            };
            try
            {
                _teacherRepository.UpdateTeacher(teacher);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
