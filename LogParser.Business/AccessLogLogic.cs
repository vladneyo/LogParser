using System;
using System.Collections.Generic;
using AutoMapper;
using LogParser.Business.Contracts;
using LogParser.Data.Contracts;
using LogParser.Data.Dtos;
using LogParser.EDM.Models;

namespace LogParser.Business
{
    // the place where several repos, services, another logics can be used for one perpose
    // but this is not case
    public class AccessLogLogic : IAccessLogLogic
    {
        private readonly IAccessLogRepository _accessLogRepository;

        public AccessLogLogic(IAccessLogRepository repo)
        {
            _accessLogRepository = repo;
        }

        public List<AccessLogDto> GetAll()
        {
            return Mapper.Map<List<AccessLogDto>>(_accessLogRepository.GetAll());
        }

        public List<AccessLogDto> GetAll(int offset, DateTime? start = null, DateTime? end = null, int limit = 10)
        {
            return Mapper.Map<List<AccessLogDto>>(_accessLogRepository.GetAll(offset, start, end, limit));
        }

        public List<string> GetTopHosts(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return _accessLogRepository.GetTopHosts(amount, start, end, decs);
        }

        public List<string> GetTopRoutes(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return _accessLogRepository.GetTopRoutes(amount, start, end, decs);
        }

        public List<AccessLogDto> CreateBulk(List<AccessLogDto> list)
        {
            return Mapper.Map<List<AccessLogDto>>(_accessLogRepository.CreateBulk(Mapper.Map<List<AccessLog>>(list)));
        }
    }
}
