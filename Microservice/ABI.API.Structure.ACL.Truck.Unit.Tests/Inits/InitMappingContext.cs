using ABI.API.Structure.Application.Infrastructure;
using AutoMapper;

namespace ABI.API.Structure.ACL.Truck.Unit.Tests.Inits
{
    public static class InitMappingContext
    {
        public static IMapper _mapper;
        public static void PrepareMaping()
        {
            var configuration = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperConfig()); });
            _mapper = configuration.CreateMapper();

        }

    }
}
