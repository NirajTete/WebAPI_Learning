using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi_Consume.Models;
using WebApi_Consume.Repository.Implementation;
using WebAPI_Learning.Data;
using WebAPI_Learning.Models;

namespace WebApi_Consume.Controllers
{
    public class RolePrivilegeController : Controller
    {
        private readonly IApiService _apiService;
        private readonly ApiSettings _apiSettings;

        public RolePrivilegeController(IApiService apiService, IOptions<ApiSettings> apiSettings)
        {
            _apiService = apiService;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Retrieve the "Bearer" claim value
                var bearerToken = User.FindFirst("Bearer")?.Value;

                var url = $"{_apiSettings.BaseUrl}/RolePrivilegeAPI/All";
                var data = await _apiService.GetAsync<APIResponse>(url, bearerToken);
                var rolePrivilege = JsonConvert.DeserializeObject<List<RolePrivilegeDTO>>(data.Data.ToString());

                return View(rolePrivilege);
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolePrivilegeDTO rolePrivilege)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the "Bearer" claim value
                    var bearerToken = User.FindFirst("Bearer")?.Value;

                    var url = $"{_apiSettings.BaseUrl}/RolePrivilegeAPI/Create";
                    var data = await _apiService.PostAsync<APIResponse>(url, rolePrivilege, bearerToken);

                    if (data != null && data.Status == true)
                    {

                        return RedirectToAction("Index");
                    }
                }

                return View(rolePrivilege);
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            if (id <= 0)
                return RedirectToAction("Index");

            // Retrieve the "Bearer" claim value
            var bearerToken = User.FindFirst("Bearer")?.Value;
            var url = $"{_apiSettings.BaseUrl}/RolePrivilegeAPI/{id}";

            var data = await _apiService.GetAsync<APIResponse>(url, bearerToken);

            if (data == null)
            {
                Console.WriteLine("No Data Found");
            }
            var rolePrivilege = JsonConvert.DeserializeObject<RolePrivilegeDTO>(data.Data.ToString());

            return View(rolePrivilege);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RolePrivilegeDTO rolePrivilege)
        {
            try
            {
                // Retrieve the "Bearer" claim value    
                var bearerToken = User.FindFirst("Bearer")?.Value;

                ////Get role name 
                var url0 = $"{_apiSettings.BaseUrl}/RoleAPI/{rolePrivilege.RoleId}";
                var result = await _apiService.GetAsync<APIResponse>(url0, bearerToken);
                var roleData = JsonConvert.DeserializeObject<RoleDTO>(result.Data.ToString());

                // Set Role Name
                rolePrivilege.RoleName = roleData.RoleName;


                var url = $"{_apiSettings.BaseUrl}/RolePrivilegeAPI/Update";
                var data = await _apiService.PutAsync<APIResponse>(url, rolePrivilege, bearerToken);

                if (data != null && data.Status == true)
                {

                    return RedirectToAction("Index");
                }


                return View(rolePrivilege);
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction("Index");

                // Retrieve the "Bearer" claim value
                var bearerToken = User.FindFirst("Bearer")?.Value;

                // Ensure the API URL is correct for deletion
                var url = $"{_apiSettings.BaseUrl}/RolePrivilegeAPI/Delete/{id}";
                var data = await _apiService.DeleteAsync<APIResponse>(url, bearerToken);

                if (data != null && data.Status == true)
                {
                    return RedirectToAction("Index");
                }
                // Optionally, you might add an error message if deletion fails
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }
        }
    }
}
