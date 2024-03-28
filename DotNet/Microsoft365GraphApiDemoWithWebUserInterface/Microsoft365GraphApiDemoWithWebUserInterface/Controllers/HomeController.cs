using Microsoft.AspNetCore.Mvc;
using Microsoft365GraphApiDemoWithWebUserInterface.Models;
using System.Diagnostics;

namespace Microsoft365GraphApiDemoWithWebUserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            GraphApiHelper graphApiHelper = new();
            await graphApiHelper.AuthenticateAsync();
            Dictionary<string, string> users = await graphApiHelper.GetUsersAsync();

            return View(users);
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
