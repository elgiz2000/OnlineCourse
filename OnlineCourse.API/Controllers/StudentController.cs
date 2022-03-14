using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;
using OnlineCourse.Entities.Dtos.Course;
using OnlineCourse.Entities.Dtos.Student;

namespace OnlineCourse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {

        private readonly StudentRepository _studentRepository;
        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository=studentRepository;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentRepository.GetStudents();
                var response = students.Select(x => new StudentGetDto
                {

                    Id = x.Id,
                    Name = x.Name,
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
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _studentRepository.GetStudentById(id);
                var response = new StudentGetDto
                {

                    Id = student.Id,
                    Name = student.Name,
                    Courses = student?.Courses?.Select(x => new CourseListDto { Id = x.Id, Name = x.Name, Price = x.Price }).ToList(),
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody]StudentDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
            };
            try
            {
                _studentRepository.AddStudent(student);         
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentRepository.DeleteStudent(id);
                return  Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent([FromBody]StudentDto dto,int id)
        {
            var student = new Student
            {
                Id=id,
                Name=dto.Name,
            };
            try
            {
                _studentRepository.UpdateStudent(student);  
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
