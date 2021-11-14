using ApiProject.Model;
using AutoMapper;
using DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a collection of Department
        /// </summary>
        /// <returns>Collection of Department</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Departments
        ///     
        /// </remarks>
        /// <response code="200">Returns the collection of Department</response>
        /// <response code="500">Internal Server Error</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                var result = await _departmentRepository.GetDepartments();
                var departmentModels = result.Select(x => _mapper.Map<DepartmentModel>(x));
                return Ok(departmentModels);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Returns a Department with collection of Employes
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <returns>Department with collection of Employes</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Departments/2
        ///     
        /// </remarks>
        /// <response code="200">Returns a Department with collection of Employes</response>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Requested Department not exist</response>
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentEmpsModel>> GetDepartment(int id)
        {
            try
            {
                var result = await _departmentRepository.GetDepartment(id);

                if (result == null)
                {
                    return NotFound();
                }

                return _mapper.Map<DepartmentEmpsModel>(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
