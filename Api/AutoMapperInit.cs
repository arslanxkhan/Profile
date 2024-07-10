using AutoMapper;
using Models.Dto;
using Models.VM;

namespace Api

{
    public class AutoMapperInit
    {

        public static IMapper InitMappings()
        {
            var config = new MapperConfiguration(cfg =>
            {
                AddMap<Models.Db.Profile, ProfileVM>(cfg);
                AddMap<Models.Db.Profile, ProfileDto>(cfg);
            });


            return new Mapper(config);
        }

        private static AddMapResult<TSource, TDest> AddMap<TSource, TDest>(IMapperConfigurationExpression cfg)
        {
            var source = cfg.CreateMap<TSource, TDest>();
            var dest = cfg.CreateMap<TDest, TSource>();
            return new AddMapResult<TSource, TDest>
            {
                Source = source,
                Destination = dest
            };

        }
    }

    class AddMapResult<TSource, TDest>
    {
        public required IMappingExpression<TSource, TDest> Source { get; set; }
        public required IMappingExpression<TDest, TSource> Destination { get; set; }
    }
}
