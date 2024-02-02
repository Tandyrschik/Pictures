using Pictures.Domain.Entities;

namespace Pictures.DAL.Interfaces
{
    public interface IPictureRepository : IRepository<Picture>
    {
        public Task<Picture> GetById(int id);
        public Task<IEnumerable<Picture>> GetAll(int accountId);
    }
}
