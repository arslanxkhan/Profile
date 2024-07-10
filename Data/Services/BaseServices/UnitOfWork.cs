using Database;
using Models.Db;


namespace Data.Services.BaseServices
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dBContext;

        public UnitOfWork(ApplicationDbContext dB)
        {
            this.dBContext = dB;
        }

        public IRepository<Profile, Guid> Profile { get; set; }

        public async Task<bool> SaveChangesAsync()
        {
            return await dBContext.SaveChangesAsync() > 0;
        }
    }
}
