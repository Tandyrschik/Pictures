using Pictures.Domain.Entities;
using Pictures.Domain.Interfaces;
using Pictures.Domain.ViewModels.Picture;

namespace Pictures.Services.Interfaces
{
	public interface IPictureService
	{
		IResponse<PictureViewModel> AddPicture(PictureViewModel pictureViewModel);
		IResponse<bool> RemovePicture(int id);
		IResponse<Picture> GetPicture(int id);
		IResponse<IEnumerable<Picture>> GetPictures();
	}
}
