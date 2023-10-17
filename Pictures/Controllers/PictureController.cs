using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Interfaces;

namespace Pictures.Controllers
{
	public class PictureController : Controller
	{
		private readonly IPictureService _pictureService;
		public PictureController(IPictureService pictureService) =>
			(_pictureService) = (pictureService);

		[HttpGet]
		public IActionResult GetPictures()
		{
			var response = _pictureService.GetPictures();
			if (response.StatusCode is Domain.Enums.StatusCode.Success)
			{
				return View(response.Data);
			}
			else return RedirectToAction("Error");
		}

		[Authorize()]
        public IActionResult RemovePicture(int id)
        {
            var response = _pictureService.RemovePicture(id);
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return RedirectToAction("GetPictures");
            }
            else return RedirectToAction("Error");
        }

        [Authorize()]
        public IActionResult AddPicture(PictureViewModel picture)
        {
            if(ModelState.IsValid)
            {
                var response = _pictureService.AddPicture(picture);
                if (response.StatusCode is Domain.Enums.StatusCode.Success)
                {
                    return RedirectToAction("GetPictures");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            else return RedirectToAction("Error");
        }
    }
}
