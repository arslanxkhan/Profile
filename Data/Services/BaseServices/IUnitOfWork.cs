using Models.Db;

namespace Data.Services.BaseServices
{
    public interface IUnitOfWork
    {
        public IRepository<Profile, Guid> Profile { get; set; }


        Task<bool> SaveChangesAsync();
    }
}
