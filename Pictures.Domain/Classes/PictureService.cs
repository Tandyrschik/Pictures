using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Interfaces;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Interfaces;

namespace Pictures.Services.Classes
{
	public class PictureService : IPictureService // в этом случае - сервиc - посредник между контроллерами и операциями с репозиторием.
	{                                            // действия в БД совершаются классом Response, объект этого же класса возвращает
												// полученные данные и информацию о том как прошла операция
		private readonly IRepository<Picture> _pictureRepository;
		public PictureService(IRepository<Picture> pictureRepository) =>
			(_pictureRepository) = (pictureRepository);

		// Добавить картинку
		public IResponse<PictureViewModel> AddPicture(PictureViewModel pictureViewModel)
		{
			var response = new Response<PictureViewModel>();
			try
			{
				var picture = new Picture
				{
					Address = pictureViewModel.Address,
					Name = pictureViewModel.Name,
                    AccountId = pictureViewModel.AccountId
				};

				_pictureRepository.Add(picture);
				response.StatusCode = StatusCode.Success;
				return response;
			}
			catch (Exception ex)
			{
				return new Response<PictureViewModel>()
				{
					Description = $"[AddPicture] : {ex.Message}",
					StatusCode = StatusCode.ServerError
				};
			}
		}

		// Удалить картинку
		public IResponse<bool> RemovePicture(int id)
		{
			var response = new Response<bool>();
			try
			{
				var picture = _pictureRepository.GetById(id);
				if (picture is null)
				{
					response.Description = "No picture found.";
					response.StatusCode = StatusCode.PictureNotFound;
				}
				else
				{
					_pictureRepository.Remove(picture);
					response.StatusCode = StatusCode.Success;
				}
				return response;
			}
			catch (Exception ex)
			{
				return new Response<bool>()
				{
					Description = $"[RemovePicture] : {ex.Message}",
					StatusCode = StatusCode.ServerError
				};
			}
		}

		// Получить картинку
		public IResponse<Picture> GetPicture(int id)
		{
			var response = new Response<Picture>();
			try
			{
				var picture = _pictureRepository.GetById(id);
				if (picture is null)
				{
					response.Description = "No picture found.";
					response.StatusCode = StatusCode.PictureNotFound;
				}
				else
				{
					response.Data = picture;
					response.StatusCode = StatusCode.Success;
				}
				return response;
			}
			catch (Exception ex)
			{
				return new Response<Picture>()
				{
					Description = $"[GetPicture] : {ex.Message}",
					StatusCode = StatusCode.ServerError
				};
			}
		}

		// Получить коллекцию картинок
		public IResponse<IEnumerable<Picture>> GetPictures()
		{
			var response = new Response<IEnumerable<Picture>>();
			try
			{
				var pictures = _pictureRepository.GetAll();

				if (pictures.Count() is 0)
				{
					response.Description = "Ни один элемент не найден.";
					response.StatusCode = StatusCode.PictureNotFound;
				}
				else
				{
					response.Data = pictures;
					response.StatusCode = StatusCode.Success;
				}
				return response;
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<Picture>>()
				{
					Description = $"[Get Pictures] : {ex.Message}",
					StatusCode = StatusCode.ServerError
				};
			}
		}
	}
}
