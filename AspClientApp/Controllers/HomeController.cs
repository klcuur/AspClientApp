using AspClientApp.Dtos;
using AspClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;


namespace AspClientApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async Task< IActionResult> Index()
		{
			var products = new List<ProductDto>();

			using (var httpClient=new HttpClient())
			{
				using( var response=await httpClient.GetAsync("https://localhost:7207/api/products"))
				{
					string apiResponse=await response.Content.ReadAsStringAsync();
					products=JsonSerializer.Deserialize<List<ProductDto>>(apiResponse);
				}
			}
			return View(products);
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
