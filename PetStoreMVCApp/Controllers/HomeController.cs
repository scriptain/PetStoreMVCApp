using Microsoft.AspNetCore.Mvc;
using PetStoreMVCApp.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace PetStoreMVCApp.Controllers
{
    // Suffix "Controller" is removed from controller name, A folder with the name "Home" is looked for in view
    // Then the method name i.e "Index" is used to map to a Index.cshtml file, thats how the framework connects controllers to views
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiCallsService _apiCallsService;
        public string selectedOption { get; set; }
        // constructor
        public HomeController(ILogger<HomeController> logger, IApiCallsService apiCallService)
        {
            _logger = logger;
            _apiCallsService = apiCallService;
        }
        // load and display the data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://petstore.swagger.io/v2/pet/findByStatus?status=available";
            var data = await _apiCallsService.GetDataFromApiAsync(apiUrl);
            Pet[]? petData = JsonConvert.DeserializeObject<Pet[]?>(data);
            ViewData["pets"] = petData;
            return View();
        }

        // handle the select tag changing
        public async Task<IActionResult> HandleSelectChange(string selectedValue)
        {
            // value selectedValue is passed from the view via the name attribute on the <select> element
            string apiUrl = "https://petstore.swagger.io/v2/pet/findByStatus?status=available";
            var data = await _apiCallsService.GetDataFromApiAsync(apiUrl);
            Pet[]? petData = JsonConvert.DeserializeObject<Pet[]?>(data);
            ViewData["selectedValue"] = selectedValue;
            ViewData["pets"] = petData;
            if (selectedValue == "atoz")
            {
                var petDataSortedByName = SortPetsByName("A", petData);
                ViewData["pets"] = petDataSortedByName;
            }
            if (selectedValue == "bycategory")
            {
                ViewData["pets"] = SortPetsByCatgeory(petData);
            }
            if (selectedValue == "bycategoryztoa")
            {
                ViewData["pets"] = SortPetsByCatgeory(SortPetsByName("D", petData));
                ViewData["pets"] = petData.OrderBy(p => p.category?.name ?? string.Empty).ThenByDescending(p => p.name).ToArray();

            }
            return View("Index"); // returning index view otherwise it will cause a 'the view handleselectchange was not found' error
        }

        // TODO: Use this function in the other actions, currently causes a bug when i try
        public async Task<Pet[]> GetPetData()
        {
            string apiUrl = "https://petstore.swagger.io/v2/pet/findByStatus?status=available";
            var data = await _apiCallsService.GetDataFromApiAsync(apiUrl);
            Pet[]? petData = JsonConvert.DeserializeObject<Pet[]?>(data);
            return petData;

        }

        public Pet[] SortPetsByName(string? order, Pet[] petData)
        {
            if(order == "D")
            {
                return petData.OrderByDescending(p => p.name).ToArray();
            } else
            {
                return petData.OrderBy(p => p.name).ToArray();

            }
        }

        public Pet[] SortPetsByCatgeory(Pet[] petData)
        {
            return petData.OrderBy(p => p.category?.name ?? string.Empty).ToArray();
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

    public class Data
    {
        // using this class to store types.
        // {"id":10002,"category":{"id":1001,"name":"dog"},"name":"gammi","photoUrls":["string"],"tags":[{"id":0,"name":"string"}],"status":"available"}
        public record Category(Int64 id, string name);
        public record Tag(Int64 id, string name);

    }
    public class Pet
    {
        public Int64 id;
        public Data.Category? category;
        public string? name;
        public string[]? photoUrls;
        public Data.Tag[]? tags;
        public string? status;

        public string? GetName()
        {
            return name;
        }
    }
}
