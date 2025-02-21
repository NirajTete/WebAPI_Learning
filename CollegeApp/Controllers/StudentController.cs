using AutoMapper;
using CollegeApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI_Learning.Data;
using WebAPI_Learning.Models;
using WebAPI_Learning.Repository.Implementation;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        //// For individual repo
        //private readonly IStudentRepository _studentRepository;

        //// For Generic repo 
        //private readonly ICollegeRepository<Student> _studentRepository;

        //// As we inherit college repo in student repo
        private readonly IStudentRepository _studentRepository;

        private APIResponse _apiResponse;

        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetStudents()
        {
            try
            {
                _logger.LogInformation("GetStudents method started");

                ////Without Repository 
                //var students = await _dbContext.Students.ToListAsync();

                var students = await _studentRepository.GetAll();

                _apiResponse.Data = _mapper.Map<List<StudentDTO>>(students); // _mapper.Map<Destination>(Source); 
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
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetStudentById(int id)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest();
                }
                ////Without Repository 
                //var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

                var student = await _studentRepository.GetByPara(student => student.Id == id);
                //NotFound - 404 - NotFound - Client error
                if (student == null)
                {
                    _logger.LogError("Student not found with given Id");
                    return NotFound($"The student with id {id} not found");
                }

                _apiResponse.Data = _mapper.Map<StudentDTO>(student);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                /*// DTO is use to show the specific data
                var studentDTO = new StudentDTO
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    Email = student.Email,
                    Address = student.Address,
                    DOB = student.DOB
                };*/


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

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetStudentByName(string name)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                ////Without Repository
                //var student = await _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefaultAsync();

                var student = await _studentRepository.GetByPara(student => student.StudentName.ToLower().Contains(name.ToLower()));

                //NotFound - 404 - NotFound - Client error
                if (student == null)
                    return NotFound($"The student with name {name} not found");

                _apiResponse.Data = _mapper.Map<StudentDTO>(student);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                /*var studentDTO = new StudentDTO
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    Email = student.Email,
                    Address = student.Address,
                    DOB = student.DOB
                };*/


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

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateStudent([FromBody] StudentDTO model)
        {

            try
            {
                //if (!ModelState.IsValid)
                //    return BadRequest(ModelState);

                if (model == null)
                    return BadRequest();

                //if(model.AdmissionDate < DateTime.Now)
                //{
                //    //1. Directly adding error message to modelstate
                //    //2. Using custom attribute
                //    ModelState.AddModelError("AdmissionDate Error", "Admission date must be greater than or equal to todays date");
                //    return BadRequest(ModelState);
                //}    

                //int newId = _dbContext.Students.LastOrDefault().Id + 1;

                Student student = _mapper.Map<Student>(model);

                /* Student student = new Student
                 {
                     //Id = newId,
                     StudentName = model.StudentName,
                     Address = model.Address,
                     Email = model.Email,
                     DOB = model.DOB
                 };*/

                await _studentRepository.Create(student);

                ////this are DB Operation already perform in repository
                //await _dbContext.Students.AddAsync(student);
                //await _dbContext.SaveChangesAsync();

                model.Id = student.Id;

                _apiResponse.Data = model;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //Status - 201
                //https://localhost:7185/api/Student/3
                //New student details
                return CreatedAtRoute("GetStudentById", new { id = model.Id }, _apiResponse);
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
        public async Task<ActionResult<APIResponse>> UpdateStudent([FromBody] StudentDTO model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    BadRequest();

                ////Without Repository
                //var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == model.Id).FirstOrDefaultAsync();

                var existingStudent = await _studentRepository.GetByPara(student => student.Id == model.Id, true);

                if (existingStudent == null)
                    return NotFound();

                var newRecord = _mapper.Map<Student>(model);

                /* var newRecord = new Student()
                 {
                     Id = existingStudent.Id,
                     StudentName = model.StudentName,
                     Email = model.Email,
                     Address = model.Address,
                     DOB = model.DOB
                 };*/

                /* existingStudent.StudentName = model.StudentName;
                existingStudent.Email = model.Email;
                existingStudent.Address = model.Address;
                existingStudent.DOB = model.DOB;*/

                await _studentRepository.Update(newRecord);

                ////this are DB Operation already perform in repository
                //_dbContext.Students.Update(newRecord);
                //await _dbContext.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }


        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        //api/student/1/updatepartial
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            try
            {
                if (patchDocument == null || id <= 0)
                    BadRequest();
                ////Without Repository
                //var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();


                var existingStudent = await _studentRepository.GetByPara(student => student.Id == id, true);

                if (existingStudent == null)
                    return NotFound();

                var studentDTO = _mapper.Map<StudentDTO>(existingStudent);

                /*  var studentDTO = new StudentDTO
                  {
                      Id = existingStudent.Id,
                      StudentName = existingStudent.StudentName,
                      Email = existingStudent.Email,
                      Address = existingStudent.Address,
                      DOB = existingStudent.DOB
                  };*/

                patchDocument.ApplyTo(studentDTO, ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                existingStudent = _mapper.Map<Student>(studentDTO);

                /* existingStudent.StudentName = studentDTO.StudentName;
                 existingStudent.Email = studentDTO.Email;
                 existingStudent.Address = studentDTO.Address;
                 existingStudent.DOB = studentDTO.DOB;*/

                await _studentRepository.Update(existingStudent);

                ////this are DB Operation already perform in repository
                //_dbContext.Update(existingStudent);
                //await _dbContext.SaveChangesAsync();

                //204 - NoContent
                return NoContent();
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }



        }


        [HttpDelete("Delete/{id}", Name = "DeleteStudentById")]
        //api/student/delete/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> DeleteStudent(int id)
        {
            try
            {
                //BadRequest - 400 - Badrequest - Client error
                if (id <= 0)
                    return BadRequest();

                ////Without Repository 
                //var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();
                var student = await _studentRepository.GetByPara(student => student.Id == id);
                //NotFound - 404 - NotFound - Client error
                if (student == null)
                    return NotFound($"The student with id {id} not found");

                await _studentRepository.Delete(student);

                _apiResponse.Data = true;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                ////this are DB Operation already perform in repository
                //_dbContext.Students.Remove(student);
                //await _dbContext.SaveChangesAsync();

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
    }
}
