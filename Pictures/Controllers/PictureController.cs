
using Microsoft.AspNetCore.Mvc;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Interfaces;
using System.Text;

namespace Pictures.Controllers
{
    public class PictureController : Controller
    {
        private readonly IPictureService _pictureService;
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PictureController(IPictureService pictureService, IAccountService accountService, IWebHostEnvironment webHostEnvironment)
        {
            _pictureService = pictureService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> MyPictures()
        {
            string login = HttpContext.User.Claims.First().Value;
            var account = await _accountService.GetAccountByLogin(login);

            var response = await _pictureService.GetPicturesOfAccount(account.Id);
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return View(response.Data);
            }
            if (response.StatusCode is Domain.Enums.StatusCode.PictureNotFound)
            {
                return RedirectToAction("AddPicture");
            }
            else return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePicture(int id)
        {
            var response = await _pictureService.RemovePicture(id);
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return RedirectToAction("MyPictures");
            }
            else return RedirectToAction("Error");
        }

        [HttpGet]
        public IActionResult AddPicture() => View(new PictureViewModel());

        [HttpPost]
        public async Task<IActionResult> AddPicture(PictureViewModel model)
        {
            if (ModelState.IsValid)
            {
                // получить id аккаунта для привязки изображений к аккаунту
                string login = HttpContext.User.Claims.First().Value;
                var account = await _accountService.GetAccountByLogin(login);

                model.AccountId = account.Id;

                // переименование сохраняемого файла, получение пути сохранения,
                var uniqueFileName = GetUniqueFileName(model.ImageFile.FileName);
                var imagesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                // сохранение файла в папку с проектом
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                model.Address = "/img/" + uniqueFileName;

                var response = await _pictureService.AddPicture(model);
                if (response.StatusCode is Domain.Enums.StatusCode.Success)
                {
                    return RedirectToAction("MyPictures");
                }
                ModelState.AddModelError("", response.Description);

            }
            return View(new PictureViewModel());
        }

        private string GetUniqueFileName(string fileName)
        {
            var sb = new StringBuilder();

            sb.Append(Path.ChangeExtension(fileName, null));
            sb.Append('_');
            sb.Append(Guid.NewGuid().ToString().Substring(0, 10));
            sb.Append('_');
            sb.Append(DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace(":", "_"));
            sb.Append(Path.GetExtension(fileName));


            return sb.ToString();
        }
    }
}
