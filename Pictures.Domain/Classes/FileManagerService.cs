
using Pictures.Domain.Enums;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Interfaces;
using System.IO.Abstractions;
using System.Text;

namespace Pictures.Services.Classes
{
    public class FileManagerService : IFileManagerService
    {
        public async Task<Response<bool>> SavePictureFile(string filePath, PictureViewModel model)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                return new Response<bool>()
                {
                    Data = true,
                    Description = "Picture was saved",
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>()
                {
                    Data = false,
                    Description = $"[SavePictureFile] : {ex.Message}",
                    StatusCode = StatusCode.ServerError
                };
            }
        }

        public string GetUniqueFileName(string fileName)
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

        public string GetSavePath(string webRootPath, string folderName, string uniqueFileName)
        {
            var imagesFolderPath = Path.Combine(webRootPath, folderName);
            var filePath = Path.Combine(imagesFolderPath, uniqueFileName);

            return filePath;
        }
    }
}
