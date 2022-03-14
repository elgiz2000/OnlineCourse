using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;
using OnlineCourse.Entities.Dtos.Course;
using OnlineCourse.Entities.Dtos.Department;

namespace OnlineCourse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {

        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                var departments = _departmentRepository.GetDepartments();
                var response = departments.Select(x => new DepartmentGetDto
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
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var student = _departmentRepository.GetDepartmentById(id);
                var response = new DepartmentGetDto
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
        public IActionResult AddDepartment([FromBody] DepartmentDto dto)
        {
            var department = new Department
            {
                Name=dto.Name,
            };
            try
            {
                _departmentRepository.AddDepartment(department);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                _departmentRepository.DeleteDepartment(id);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment([FromBody] DepartmentDto dto,int id)
        {
            var department = new Department
            {
                Id = id,
                Name = dto.Name,
            };
            try
            {
                _departmentRepository.UpdateDepartment(department);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
