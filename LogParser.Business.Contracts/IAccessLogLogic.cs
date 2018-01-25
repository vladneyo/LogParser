using LogParser.Data.Dtos;
using System;
using System.Collections.Generic;

namespace LogParser.Business.Contracts
{
    public interface IAccessLogLogic
    {
        List<AccessLogDto> GetAll();

        List<AccessLogDto> GetAll(int offset, DateTime? start = null, DateTime? end = null, int limit = 10);

        List<string> GetTopHosts(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false);

        List<string> GetTopRoutes(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false);

        List<AccessLogDto> CreateBulk(List<AccessLogDto> list);
    }
}
