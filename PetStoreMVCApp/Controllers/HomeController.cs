using Microsoft.AspNetCore.Mvc;
using PetStoreMVCApp.Models;
using System.Diagnostics;

namespace PetStoreMVCApp.Controllers
{
// Suffix "Controller" is removed from controller name, A folder with the name "Home" is looked for in view
// Then the method name i.e "Index" is used to map to a Index.cshtml file, thats how the framework connects controllers to views
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiCallsService _apiCallsService;

        public HomeController(ILogger<HomeController> logger, IApiCallsService apiCallService)
        {
            _logger = logger;
            _apiCallsService = apiCallService;
        }

        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://petstore.swagger.io/v2/pet/findByStatus?status=available";
            var data = await _apiCallsService.GetDataFromApiAsync(apiUrl);
            ViewData["ApiData"] = data;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
