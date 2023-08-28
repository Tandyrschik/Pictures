using Pictures.Domain.Enums;
using Pictures.Domain.Interfaces;

namespace Pictures.Domain.Responses
{
	public class Response<T> : IResponse<T>
	{
		public string Description { get; set; }
		public StatusCode StatusCode { get; set; }
		public T Data { get; set; } 
	}
}
