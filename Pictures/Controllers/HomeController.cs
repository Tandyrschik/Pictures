using Microsoft.AspNetCore.Mvc;
using Pictures.Models;
using Pictures.Services.Interfaces;
using System.Diagnostics;

namespace Pictures.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPictureService _pictureService;

        public HomeController(ILogger<HomeController> logger, IPictureService pictureService)
        {
            _logger = logger;
            _pictureService = pictureService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _pictureService.GetAllPictures();
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return View(response.Data);
            }
            else return RedirectToAction("Error");
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