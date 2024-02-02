using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Picture;
using System.Text;

namespace Pictures.Services.Interfaces
{
    public interface IFileManagerService
    {
        Task<Response<bool>> SavePictureFile(string path, PictureViewModel model);
        public string GetUniqueFileName(string fileName);
        public string GetSavePath(string webRootPath, string folderName, string uniqueFileName);
    }
}
