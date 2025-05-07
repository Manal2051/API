using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestToken.DTO.EmployeeDto;
using TestToken.Models;
using TestToken.UOW;

namespace TestToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Employees")]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _unitOfWork.Employees.GetAllEmployees(pageNumber, pageSize);

            if (response.IsSucceeded)
                return Ok(response);

            return StatusCode(response.StatusCode, new { response.Message });
        }

        //[Authorize(Policy = "Admin")]
        [HttpGet("EmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _unitOfWork.Employees.GetEmployeeById(id);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode, new { response.Message });
        }

        [HttpGet("EmployeeByName")]
        public async Task<IActionResult> GetEmployeeByName([FromQuery]string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _unitOfWork.Employees.GetEmployeeByName(name);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode, new { response.Message });
        }

       
       // [Authorize(Policy = "Admin")]
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _unitOfWork.Employees.AddEmployee(employeeDto);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode, new { response.Message });
        }

       // [Authorize(Policy = "Admin")]
        [HttpPut("EditEmployee")]
        public async Task<IActionResult> EditEmployee(int id ,EmployeeDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _unitOfWork.Employees.UpdateEmployee(id,productDto);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode, new { response.Message });
        }

       // [Authorize(Policy = "Admin")]
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _unitOfWork.Employees.DeleteEmployee(id);
            if (response.IsSucceeded)
                return Ok(response);
            return StatusCode(response.StatusCode, new { response.Message });
        }
    }
}
