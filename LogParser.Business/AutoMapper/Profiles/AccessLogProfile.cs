using AutoMapper;
using LogParser.Business.AutoMapper.Resolvers;
using LogParser.Data.Dtos;
using LogParser.EDM.Models;

namespace LogParser.Business.AutoMapper.Profiles
{
    public class AccessLogProfile : Profile
    {
        public AccessLogProfile()
        {
            CreateMap<AccessLog, AccessLogDto>()
                .ForMember(x => x.Time, opt => opt.ResolveUsing(new AccessLogDatetimeResolver()));
            CreateMap<AccessLogDto, AccessLog>()
                .ForMember(x => x.Time, opt => opt.ResolveUsing(new AccessLogDatetimeResolver()));
        }
    }
}
