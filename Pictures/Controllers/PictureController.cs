using Microsoft.AspNetCore.Mvc;
using Pictures.Services.Interfaces;

namespace Pictures.Controllers
{
	public class PictureController : Controller
	{
		private readonly IPictureService _pictureService;
		public PictureController(IPictureService pictureService)
		{
			_pictureService = pictureService;
		}

		[HttpGet]
		public IActionResult GetPictures()
		{
			var response = _pictureService.GetPictures();
			if (response.StatusCode == Domain.Enums.StatusCode.Success)
			{
				return View(response.Data);
			}
			else return RedirectToAction("Error");
		}
	}
}
