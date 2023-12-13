using Pictures.Domain.Entities;
using Pictures.Domain.Interfaces;
using Pictures.Domain.ViewModels.Picture;

namespace Pictures.Services.Interfaces
{
	public interface IPictureService
	{
		Task<IResponse<PictureViewModel>> AddPicture(PictureViewModel pictureViewModel);
		Task<IResponse<bool>> RemovePicture(int id);
		Task<IResponse<Picture>> GetPicture(int id);
		Task<IResponse<IEnumerable<Picture>>> GetAllPictures();
		Task<IResponse<IEnumerable<Picture>>> GetPicturesOfAccount(int accountId);
    }
}
