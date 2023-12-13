using Pictures.Domain.Enums;

namespace Pictures.Domain.Interfaces
{
	public interface IResponse<T>
	{
		string Description { get; set; }
		StatusCode StatusCode { get; set; }
		T Data { get; set; }
	}
}
