using AutoMapper;
using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using WebAPI_Learning.Data;
using WebAPI_Learning.Models;
using WebAPI_Learning.Repository.Implementation;
using WebAPI_Learning.Repository.Service;

namespace WebAPI_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAPIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private APIResponse _apiResponse;
        private readonly ILogger<RoleAPIController> _logger;

        public RoleAPIController(IMapper mapper, ICollegeRepository<Role> roleRepository, ILogger<RoleAPIController> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _apiResponse = new();
            _logger = logger;
        }

        [HttpGet]
        [Route("All", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetAllRoles()
        {
            try
            {
                _logger.LogInformation("GetAll roles method started");
                ////Without Repository 
                //var students = await _dbContext.Students.ToListAsync();

                var roles = await _roleRepository.GetAll();

                if (roles.Count == 0)
                    return NotFound("No Data Found");

                _apiResponse.Data = _mapper.Map<List<RoleDTO>>(roles); // _mapper.Map<Destination>(Source); 
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                /* // DTO is use to show the specific data
                 var students = await _dbContext.Students.Select(s => new StudentDTO()
                 {
                     Id = s.Id,
                     StudentName = s.StudentName,
                     Address = s.Address,
                     Email = s.Email,
                     DOB = s.DOB
                 }).ToListAsync();*/

                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;

            }


        }

        [HttpGet]
        [Route("{id:int}", Name = "GetRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetRoleById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var role = await _roleRepository.GetByPara(role => role.Id == id);

                if (role == null)
                {
                    return NotFound($"The role with id: {id} is not found");
                }

                _apiResponse.Data = _mapper.Map<RoleDTO>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetRoleByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetRoleByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest();
                }

                var role = await _roleRepository.GetByPara(role => role.RoleName == name);

                if (role == null)
                {
                    return NotFound($"The role with id: {name} is not found");
                }

                _apiResponse.Data = _mapper.Map<RoleDTO>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateRole(RoleDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                Role role = _mapper.Map<Role>(model);
                role.IsDeleted = false;
                role.CreatedDate = DateTime.Now;
                role.ModifiedDate = DateTime.Now;

                var result = await _roleRepository.Create(role);
                model.Id = result.Id;

                _apiResponse.Status = true;
                _apiResponse.Data = result;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //return Ok(_apiResponse);
                return CreatedAtRoute("GetRoleById", new { id = model.Id }, _apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

         [HttpPut]
        [Route("Update")]
        //api/student/update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> Update(RoleDTO model)
        {
            try
            {
                if (model == null || model.Id == 0)
                    return BadRequest();

                var existingRole = await _roleRepository.GetByPara(role => role.Id == model.Id, true);

                if (existingRole == null)
                    return BadRequest($"Role not found with id: {model.Id} to update");

                var newRole = _mapper.Map<Role>(model);
                newRole.ModifiedDate = DateTime.Now;

                await _roleRepository.Update(newRole);

                _apiResponse.Status = true;
                _apiResponse.Data = newRole;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpDelete]
        [Route("Delete/{id}", Name ="DeleteById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if(id <= 0)
                    return BadRequest();

                var role = await _roleRepository.GetByPara(role => role.Id == id, true);

                if (role == null)
                    return BadRequest($"No Role Found with the id: {id}");

                await _roleRepository.Delete(role);

                _apiResponse.Status = true;
                _apiResponse.Data = true;
                _apiResponse.StatusCode= HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }


    }
}
