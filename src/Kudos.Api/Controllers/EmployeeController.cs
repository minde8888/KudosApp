using Kudos.Services.Dtos;
using Kudos.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kudos.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Creates a Employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>A newly created Employee</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     POST /api/v1/Employee
        ///     {
        ///       "isSuccessfull": true,
        ///        "error": null,
        ///        "employee": {
        ///             "id": int,
        ///             "name": "string",
        ///             "surname": "string"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created employee</response>     
        [HttpPost]
        public async Task<IActionResult> Add(EmployeeRequest employee)
        {
            var result = await _employeeService.AddNewEmployeeAsync(employee);
            return Ok(result);
        }

        /// <summary>
        /// Get all Employees.
        /// </summary>
        /// <returns>Get all employees from data base</returns>
        /// <remarks>
        /// Sample response:
        ///
        ///     Get /api/v1/Employee
        ///     [{
        ///         "id": int,
        ///         "name": "string",
        ///         "surname": "string"
        ///     },
        ///     {
        ///        "id": int,
        ///        "name": "string",
        ///        "surname": "string"
        ///     }]
        ///
        /// </remarks>
        /// <response code="200">Returns all employees</response>
        /// <response code="404">If the employee is not found</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employeeService.GetAllEmployeeAsync();
            return Ok(result);
        }
    }
}