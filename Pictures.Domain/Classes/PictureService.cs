﻿
using Microsoft.AspNetCore.Identity;
using Pictures.DAL.Interfaces;
using Pictures.Domain.Entities;
using Pictures.Domain.Enums;
using Pictures.Domain.Interfaces;
using Pictures.Domain.Responses;
using Pictures.Domain.ViewModels.Picture;
using Pictures.Services.Interfaces;

namespace Pictures.Services.Classes
{
	public class PictureService : IPictureService // сервиc - посредник между контроллерами и операциями с репозиторием.
	{                                            // действия в БД совершаются классом Response, объект этого же класса возвращает
												// полученные данные и информацию о том как прошла операция
		private readonly IPictureRepository _pictureRepository;
		public PictureService(IPictureRepository pictureRepository) =>
			(_pictureRepository) = (pictureRepository);

        // Добавить картинку
        public async Task<IResponse<PictureViewModel>> AddPicture(PictureViewModel pictureViewModel)
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

				await _pictureRepository.Add(picture);
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
		public async Task<IResponse<bool>> RemovePicture(int id)
		{
			var response = new Response<bool>();
			try
			{
				var picture = await _pictureRepository.GetById(id);
				if (picture is null)
				{
					response.Description = "No picture found.";
					response.StatusCode = StatusCode.PictureNotFound;
				}
				else
				{
					await _pictureRepository.Remove(picture);
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
		public async Task<IResponse<Picture>> GetPicture(int id)
		{
			var response = new Response<Picture>();
			try
			{
				var picture = await _pictureRepository.GetById(id);
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
		public async Task<IResponse<IEnumerable<Picture>>> GetAllPictures()
		{
			var response = new Response<IEnumerable<Picture>>();
			try
			{
				var pictures = await _pictureRepository.GetAll();

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

        // Получить коллекцию картинок одного аккаунта
        public async Task<IResponse<IEnumerable<Picture>>> GetPicturesOfAccount(int accountId)
        {
            var response = new Response<IEnumerable<Picture>>();
            try
            {
                var pictures = await _pictureRepository.GetAllOfAccount(accountId);

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
