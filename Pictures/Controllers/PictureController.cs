
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Classes;
using Pictures.Services.Interfaces;
using System.Text;

namespace Pictures.Controllers
{
    public class PictureController : Controller
    {
        private readonly IPictureService _pictureService;
        private readonly IAccountService _accountService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PictureController(IPictureService pictureService, IAccountService accountService, 
               IFileManagerService fileManagerService, IWebHostEnvironment webHostEnvironment)
        {
            _pictureService = pictureService;
            _accountService = accountService;
            _fileManagerService = fileManagerService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> MyPictures()
        {
            string login = ExtractAccountLoginFromClaims();
            var account = await _accountService.GetAccountByLogin(login);

            var response = await _pictureService.GetPicturesOfAccount(account.Id);
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return View(response.Data);
            }
            if (response.StatusCode is Domain.Enums.StatusCode.NotFound)
            {
                return RedirectToAction("AddPicture","Picture");
            }
            else return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> RemovePicture(int id) // id приходит из представления
        {
            var response = await _pictureService.RemovePicture(id);
            if (response.StatusCode is Domain.Enums.StatusCode.Success)
            {
                return RedirectToAction("MyPictures", "Picture");
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
                string login = ExtractAccountLoginFromClaims();
                var account = await _accountService.GetAccountByLogin(login);

                // переименование сохраняемого файла, получение пути сохранения,
                string uniqueFileName = _fileManagerService.GetUniqueFileName(model.ImageFile.FileName);
                string folderName = "img";
                string filePath = _fileManagerService.GetSavePath(_webHostEnvironment.WebRootPath, folderName, uniqueFileName);

                // сохранение файла в папку с проектом
                var fileManagerServiceResponse = await _fileManagerService.SavePictureFile(filePath, model);

                if (fileManagerServiceResponse.StatusCode is Domain.Enums.StatusCode.Success)
                {
                    model.AccountId = account.Id;
                    model.Address = "/img/" + uniqueFileName;

                    var response = await _pictureService.AddPicture(model);
                    if (response.StatusCode is Domain.Enums.StatusCode.Success)
                    {
                        return RedirectToAction("MyPictures", "Picture");
                    }
                    else ModelState.AddModelError("", response.Description);
                }
                else ModelState.AddModelError("", fileManagerServiceResponse.Description);
            }
            return View(new PictureViewModel());
        }

        private string ExtractAccountLoginFromClaims()
        {
            return HttpContext.User.Claims.First().Value;
        }
    }
}
