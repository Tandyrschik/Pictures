

namespace Pictures.Domain.Enums
{
	public enum StatusCode // перечисление для отслеживания и сохранения информации о статусе прошедших операций.
	{                     //  применяется для элементов в папке Domain.Responses
		Success = 200,
		PictureNotFound = 404,
		ServerError = 500
	}
}
