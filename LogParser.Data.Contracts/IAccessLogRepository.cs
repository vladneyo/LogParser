using System;
using System.Collections.Generic;
using LogParser.EDM.Models;

namespace LogParser.Data.Contracts
{
    public interface IAccessLogRepository
    {
        List<AccessLog> GetAll();

        List<AccessLog> GetAll(int offset, DateTime? start = null, DateTime? end = null, int limit = 10);

        List<string> GetTopHosts(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false);

        List<string> GetTopRoutes(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false);

        List<AccessLog> CreateBulk(List<AccessLog> list);
    }
}
