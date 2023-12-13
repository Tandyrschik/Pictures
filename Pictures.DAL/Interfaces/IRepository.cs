

namespace Pictures.DAL.Interfaces
{
	public interface IRepository<T> // базовый интерфейс для общих для всех объектов операций с БД
	{                              // в <T> помещаются классы моделей(Picture или Account)
		Task<bool> Add(T entity);
		Task<bool> Remove(T entity);
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
	}
}
