using Pictures.Domain.Enums;

namespace Pictures.Domain.Interfaces
{
	public interface IResponse<T>
	{
		string Description { get; set; }
		StatusCode StatusCode { get; set; }
		T Data { get; set; } // отсутствие универсального типа для подобных сущностей неприемлимо, так как: 
							 // 1 - запросы/ответы(Response) также рассчитаны на сущности, которые ещё не созданы.
							 // 2 - object будет тратить ресурсы приложения на упаковку и распаковку
	}
}
