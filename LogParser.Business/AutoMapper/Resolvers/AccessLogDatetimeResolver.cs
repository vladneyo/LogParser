using System;
using AutoMapper;
using LogParser.Data.Converters;
using LogParser.Data.Dtos;
using LogParser.EDM.Models;

namespace LogParser.Business.AutoMapper.Resolvers
{
    public class AccessLogDatetimeResolver: IValueResolver<AccessLogDto, AccessLog, DateTime>, IValueResolver<AccessLog, AccessLogDto, string>
    {

        public string Resolve(AccessLog source, AccessLogDto destination, string destMember, ResolutionContext context)
        {
            destMember = new AccessLogDatetimeConverter().Convert(source.Time);
            destination.Time = destMember;
            return destMember;
        }

        public DateTime Resolve(AccessLogDto source, AccessLog destination, DateTime destMember, ResolutionContext context)
        {
            destMember = new AccessLogDatetimeConverter().Convert(source.Time);
            destination.Time = destMember;
            return destMember;
        }
    }
}
