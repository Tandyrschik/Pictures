
namespace Pictures.DAL.Interfaces
{
	public interface IRepository<T> // базовый интерфейс для общих для всех объектов операций с БД
	{                              // в <T> помещаются классы моделей(Picture или Account)
		public Task<bool> Add(T entity);
        public Task<bool> Remove(T entity);
        public Task<IEnumerable<T>> GetAll();
	}
}
