

namespace Pictures.Domain.Enums
{
	public enum StatusCode // перечисление для отслеживания и сохранения информации о статусе прошедших операций.
	{                     //  применяется для элементов в папке Domain.Responses
		Success = 0,
		NotFound = 1,
		ServerError = 2,
		SpecifiedDataExist = 3,
		IncorrectData = 4,
	}
}
