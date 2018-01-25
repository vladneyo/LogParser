using System;
using System.Collections.Generic;
using AutoMapper;
using LogParser.Business.Contracts;
using LogParser.Data.Contracts;
using LogParser.Data.Dtos;
using LogParser.EDM.Models;

namespace LogParser.Business
{
    // place where several repos can be used for one perpose
    // but this is not case
    public class AccessLogLogic : IAccessLogLogic
    {
        private IAccessLogRepository _repo;

        public AccessLogLogic(IAccessLogRepository repo)
        {
            _repo = repo;
        }

        public List<AccessLogDto> GetAll()
        {
            return Mapper.Map<List<AccessLogDto>>(_repo.GetAll());
        }

        public List<AccessLogDto> GetAll(int offset, DateTime? start = null, DateTime? end = null, int limit = 10)
        {
            return Mapper.Map<List<AccessLogDto>>(_repo.GetAll(offset, start, end, limit));
        }

        public List<string> GetTopHosts(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return Mapper.Map<List<string>>(_repo.GetTopHosts(amount, start, end, decs));
        }

        public List<string> GetTopRoutes(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return Mapper.Map<List<string>>(_repo.GetTopRoutes(amount, start, end, decs));
        }

        public List<AccessLogDto> CreateBulk(List<AccessLogDto> list)
        {
            return Mapper.Map<List<AccessLogDto>>(_repo.CreateBulk(Mapper.Map<List<AccessLog>>(list)));
        }
    }
}
