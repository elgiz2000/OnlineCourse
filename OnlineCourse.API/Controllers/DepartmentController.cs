using Microsoft.AspNetCore.Mvc;
using OnlineCourse.CRUD.Repositories;
using OnlineCourse.Data;
using OnlineCourse.Entities.Dtos;

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
        public IActionResult GetAllStudents()
        {
            try
            {
                var departments = _departmentRepository.GetDepartments();
                return Ok(departments);
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
                return Ok(student);
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
