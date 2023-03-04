using AutoMapper;
using Kernel.Entity;
using Kernel;
using Kernel.Request;
using Kernel.Response;


namespace Kernel.Mapper
{
    public class AutoMapping
    {
        public static IMapper mapperResponse;
        public static IMapper mapperRequest;
        public static void Inicializer()
        {
            var configurationResponse = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, ProdutoResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            });

            var configurationRequest = new MapperConfiguration(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.CreateMap<ProdutoRequest, Produto>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            });

            mapperResponse = configurationResponse.CreateMapper();
            mapperRequest = configurationRequest.CreateMapper();

            
        }
    }
}
