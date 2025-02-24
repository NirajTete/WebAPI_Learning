using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI_Learning.Data;
using WebAPI_Learning.Models;
using WebAPI_Learning.Repository.Implementation;

namespace WebAPI_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrivilegeAPIController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<RolePrivilege> _rolePrivilegeRepository;
        private APIResponse _apiResponse;
        private readonly ILogger<RolePrivilegeAPIController> _logger;

        public RolePrivilegeAPIController(IMapper mapper, ICollegeRepository<RolePrivilege> rolePrivilegeRepository, ILogger<RolePrivilegeAPIController> logger)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _apiResponse = new();
            _logger = logger;
        }


        [HttpGet]
        [Route("All", Name = "GetAllRolesPrivilege")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetAllRolesPrivilege()
        {
            try
            {
                _logger.LogInformation("GetAll roles method started");
                ////Without Repository 
                //var students = await _dbContext.Students.ToListAsync();

                var rolesPrivilege = await _rolePrivilegeRepository.GetAll();

                if (rolesPrivilege.Count == 0)
                    return NotFound("No Data Found");

                _apiResponse.Data = _mapper.Map<List<RolePrivilegeDTO>>(rolesPrivilege); // _mapper.Map<Destination>(Source); 
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
        [Route("{id:int}", Name = "GetRolePrivilegeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetRolePrivilegeById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var rolePrivilege = await _rolePrivilegeRepository.GetByPara(rolePrivilege => rolePrivilege.Id == id);

                if (rolePrivilege == null)
                {
                    return NotFound($"The role with id: {id} is not found");
                }

                _apiResponse.Data = _mapper.Map<RolePrivilegeDTO>(rolePrivilege);
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
        [Route("{name:alpha}", Name = "GetRolePrivilegeByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetRolePrivilegeByName(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest();
                }

                var rolePrivilege = await _rolePrivilegeRepository.GetByPara(rolePrivilege => rolePrivilege.RolePrivilegeName == name);

                if (rolePrivilege == null)
                {
                    return NotFound($"The role with id: {name} is not found");
                }

                _apiResponse.Data = _mapper.Map<RolePrivilegeDTO>(rolePrivilege);
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
        public async Task<ActionResult<APIResponse>> CreateRole(RolePrivilegeDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                RolePrivilege rolePrivilege = _mapper.Map<RolePrivilege>(model);
                rolePrivilege.IsDeleted = false;
                rolePrivilege.CreatedDate = DateTime.Now;
                rolePrivilege.ModifiedDate = DateTime.Now;

                var result = await _rolePrivilegeRepository.Create(rolePrivilege);
                model.Id = result.Id;

                _apiResponse.Status = true;
                _apiResponse.Data = result;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //return Ok(_apiResponse);
                return CreatedAtRoute("GetRolePrivilegeById", new { id = model.Id }, _apiResponse);

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

                var existingRolePrivilege = await _rolePrivilegeRepository.GetByPara(rolePrivilege => rolePrivilege.Id == model.Id, true);

                if (existingRolePrivilege == null)
                    return BadRequest($"Role not found with id: {model.Id} to update");

                var newRolePrivilege = _mapper.Map<RolePrivilege>(model);
                newRolePrivilege.ModifiedDate = DateTime.Now;

                await _rolePrivilegeRepository.Update(newRolePrivilege);

                _apiResponse.Status = true;
                _apiResponse.Data = newRolePrivilege;
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
        [Route("Delete/{id}", Name = "DeletePrivilegeById")]
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
                if (id <= 0)
                    return BadRequest();

                var rolePrivilege = await _rolePrivilegeRepository.GetByPara(rolePrivilege => rolePrivilege.Id == id, true);

                if (rolePrivilege == null)
                    return BadRequest($"No Role Privilege Found with the id: {id}");

                await _rolePrivilegeRepository.Delete(rolePrivilege);

                _apiResponse.Status = true;
                _apiResponse.Data = true;
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

    }
}
