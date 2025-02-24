using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebApi_Consume.Models;
using WebApi_Consume.Repository.Implementation;
using WebAPI_Learning.Models;

namespace WebApi_Consume.Controllers
{
    public class RoleController : Controller
    {
        private readonly IApiService _apiService;
        private readonly ApiSettings _apiSettings;

        public RoleController(IApiService apiService, IOptions<ApiSettings> apiSettings)
        {
            _apiService = apiService;
            _apiSettings = apiSettings.Value;
        }

        // GET: CountryMaster
        public async Task<IActionResult> Index()
        {
            try
            {
                // Retrieve the "Bearer" claim value
                var bearerToken = User.FindFirst("Bearer")?.Value;

                var url = $"{_apiSettings.BaseUrl}/Role/All";
                var data = await _apiService.GetAsync<APIResponse>(url, bearerToken);
                var role = JsonConvert.DeserializeObject<List<RoleDTO>>(data.Data.ToString());

                return View(role);
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }
        }
        // GET: CountryMaster/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDTO role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the "Bearer" claim value
                    var bearerToken = User.FindFirst("Bearer")?.Value;

                    var url = $"{_apiSettings.BaseUrl}/Role/Create";
                    var data = await _apiService.PostAsync<APIResponse>(url, role, bearerToken);

                    if (data != null && data.Status == true)
                    {

                        return RedirectToAction("Index");
                    }
                }

                return View(role);
            }
            catch (Exception)
            {
                // Redirect to the internal server error page
                return RedirectToAction("InternalServerError", "Error");
            }


        }
    }
}
