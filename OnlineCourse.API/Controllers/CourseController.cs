using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;

namespace OnlineCourse.API.Controllers
{
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly CourseRepository _courseRepository = new();
        public CourseController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                var courses = _courseRepository.GetCourses();
                return Ok(courses);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                var course = _courseRepository.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Price = dto.Price,
                TeacherId=dto.TeacherId,
                DepartmentId=dto.DepartmentId,
            };
            try
            {
                _courseRepository.AddCourse(course);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                _courseRepository.DeleteCourse(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher([FromBody] CourseDto dto, int id)
        {
            var course = new Course
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price,
                TeacherId = dto.TeacherId,
                DepartmentId = dto.DepartmentId,
            };
            try
            {
                _courseRepository.UpdateCourse(course);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
