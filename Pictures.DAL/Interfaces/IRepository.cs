

namespace Pictures.DAL.Interfaces
{
	public interface IRepository<T> // базовый интерфейс для общих для всех объектов операций с БД
	{                              // в <T> помещаются классы моделей(Picture или Account)
		bool Add(T entity);
		bool Remove(T entity);
		T GetById(int id);
		IEnumerable<T> GetAll();
	}
}
