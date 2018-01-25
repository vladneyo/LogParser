using AutoMapper;
using LogParser.API.DependencyResolution;
using LogParser.Business.AutoMapper.Profiles;

namespace LogParser.API.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.ConstructServicesUsing(t => IoC.Initialize().GetInstance(t));
                x.AddProfile<AccessLogProfile>();
            });
        }
    }
}