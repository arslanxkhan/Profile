using AutoMapper;

namespace Data.Services.BaseServices
{
    public class BaseService
    {
        public required IUnitOfWork UnitOfWork { get; set; }
        public required IMapper Mapper { get; set; }
    }
}
