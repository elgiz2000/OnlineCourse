using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;

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
                return Ok(teachers);
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
                return Ok(teacher);
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
