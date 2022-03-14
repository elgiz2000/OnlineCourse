using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Entities.Dtos;

namespace OnlineCourse.API.Controllers
{
    [Route("api/")]
    public class CourseStudentController : Controller
    {
        private readonly CourseStudentRepository _courseStudentRepository;
        public CourseStudentController(CourseStudentRepository courseStudentRepository)
        {
            _courseStudentRepository=courseStudentRepository;
        }
        [HttpGet("Course/{id}/Students")]
        public IActionResult GetAllStudents(int id)
        {
            try
            {
                var courses = _courseStudentRepository.GetStudents(id);
                return Ok(courses);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
        [HttpDelete("Course/{courseId}/Students/{studentId}")]
        public IActionResult DeleteStudent(int courseid,int studentid)
        {
            var course_student = new CourseStudentDto
            {
                CourseId = courseid,
                StudentId = studentid,
            };
            try
            {
                _courseStudentRepository.DeleteStudent(course_student);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPost("Course/{courseId}/Students")]
        public IActionResult AddStudent([FromBody] int studentId,int courseId)
        {
            var course_student = new CourseStudentDto
            {
                CourseId = courseId,
                StudentId = studentId,
            };
            try
            {
                _courseStudentRepository.AddStudent(course_student);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
