using ApiProject.Model;
using ApiProject.Services;
using AutoMapper;
using DAL.Domain;
using DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBlobsService _blobsService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, IBlobsService blobsService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _blobsService = blobsService;
        }

        /// <summary>
        /// Returns a collection of Employees matching the given params.
        /// </summary>
        /// <param name="search">The first name or the last name to search for</param>
        /// <param name="departmentId">The Id of the Department</param>
        /// <param name="gender">Gender of the employee ex. (Male/0, Female/1, Other/2)</param>
        /// <returns>Collection of Employees</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Employees/search?search=name&departmentId=2&gender=1
        ///
        /// </remarks>
        /// <response code="200">Returns the collection of Employees</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> Search([FromQuery] string search, int departmentId, Gender? gender)
        {
            try
            {
                var result = await _employeeRepository.Search(search, gender, departmentId);

                var employeeModels = result.Select(x => _mapper.Map<EmployeeModel>(x));

                return Ok(employeeModels);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// get collection of employees by department id
        /// </summary>
        /// <param name="departmentId">id of the department of the employee</param>
        /// <returns>collection of employees</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Employees/GetEmployeeByDepartmentId/2
        ///
        /// </remarks>
        /// <response code="200">Returns the collection of Employee</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Department Id is required</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetEmployeeByDepartmentId/{departmentId:int}")]
        public ActionResult<IEnumerable<EmployeeModel>> FilterEmployee(int departmentId)
        {
            try
            {
                if (departmentId <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Department Id is required");
                }
                var result = _employeeRepository.GetEmployeesByCustomeFilter(x => x.DepartmentId == departmentId);

                var employeeModels = result.Select(x => _mapper.Map<EmployeeModel>(x));

                return Ok(employeeModels);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// get all the employees
        /// </summary>
        /// <returns>collection of employees</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Employees
        ///
        /// </remarks>
        /// <response code="200">Returns the collection of Employee</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                var employeeModels = employees.Select(x => _mapper.Map<EmployeeModel>(x));
                return Ok(employeeModels);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// get employee by id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Employees/2
        ///
        /// </remarks>
        /// <response code="200">Returns the Employee</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Request Resource not found</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);

                if (result == null)
                {
                    return NotFound();
                }

                return _mapper.Map<EmployeeModel>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// create new employee
        /// </summary>
        /// <param name="employee">employee object</param>
        /// <returns>created employee url</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Employees
        ///     {
        ///         "id": 0,
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "string",
        ///         "dateOfBrith": "2021-11-11T02:55:45.420Z",
        ///         "gender": 0,
        ///         "departmentId": 0,
        ///         "photoPath": "string",
        ///         "department": {
        ///             "id": 0,
        ///             "departmentName": "string",
        ///             "employees": [
        ///                 null
        ///             ]
        ///         }
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Request Body can not Null Or Email is in use</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        /// <summary>
        /// update employee details
        /// </summary>
        /// <param name="id">id of the employee</param>
        /// <param name="employee">employee object</param>
        /// <returns>updated employee object</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Employees/2
        ///     {
        ///         "id": 2,
        ///         "firstName": "string",
        ///         "lastName": "string",
        ///         "email": "string",
        ///         "dateOfBrith": "2021-11-11T02:55:45.420Z",
        ///         "gender": 0,
        ///         "departmentId": 0,
        ///         "photoPath": "string",
        ///         "department": {
        ///             "id": 0,
        ///             "departmentName": "string",
        ///             "employees": [
        ///                 null
        ///             ]
        ///         }
        ///     }
        /// </remarks>
        /// <response code="200">Returns the updated item</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Employee ID mismatch</response>
        /// <response code="404">Employee not exist with given ID</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await _employeeRepository.GetEmployee(id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }
                
                var result = await _employeeRepository.UpdateEmployee(employee);
                
                return Ok(_mapper.Map<EmployeeModel>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record");
            }
        }

        /// <summary>
        /// Delete Employee by Id
        /// </summary>
        /// <param name="id">Id of the Employee</param>
        /// <returns>Success message</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Employees/8
        ///
        /// </remarks>
        /// <response code="200">Returns the string message</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Employee not exist with given ID</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await _employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                await _employeeRepository.DeleteEmployee(id);

                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }


        [HttpPost("upload", Name = "upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadFile(
         IFormFile file)
        {
            string result = await _blobsService.UploadFile(file);
            return Ok(result);
        }
    }
}
