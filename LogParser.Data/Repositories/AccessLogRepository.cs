using System;
using System.Collections.Generic;
using System.Linq;
using LogParser.Data.Contracts;
using LogParser.EDM;
using LogParser.EDM.Models;

namespace LogParser.Data.Repositories
{
    public class AccessLogRepository : IAccessLogRepository
    {
        public List<AccessLog> CreateBulk(List<AccessLog> list)
        {
            using (ParserContext ctx = new ParserContext())
            {
                ctx.AccessLogs.AddRange(list);
                ctx.SaveChanges();
                return list;
            }
        }
        
        public List<AccessLog> GetAll()
        {
            using (ParserContext ctx = new ParserContext())
            {
                return ctx.AccessLogs.ToList();
            }
        }

        public List<AccessLog> GetAll(int offset, DateTime? start = null, DateTime? end = null, int limit = 10)
        {
            using (ParserContext ctx = new ParserContext())
            {
                var query = ctx.AccessLogs
                    .OrderBy(x => x.Time)
                    .AsQueryable();

                query = FilterByDates(query, start, end);

                return query
                    .Skip(offset)
                    .Take(limit)
                    .ToList();
            }
        }

        public List<string> GetTopHosts(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return GetTop(x => x.Host, amount, start, end, decs);
        }

        public List<string> GetTopRoutes(int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            return GetTop(x => x.Route, amount, start, end, decs);
        }

        private IQueryable<AccessLog> FilterByDates(IQueryable<AccessLog> query, DateTime? start, DateTime? end)
        {
            if (start != null)
            {
                query = query.Where(x => x.Time >= start);
            }
            if (end != null)
            {
                query = query.Where(x => x.Time <= end);
            }
            return query;
        }

        private List<string> GetTop(Func<AccessLog, string> exp, int amount = 10, DateTime? start = null, DateTime? end = null, bool decs = false)
        {
            using (ParserContext ctx = new ParserContext())
            {
                var query = ctx.AccessLogs.AsQueryable();

                query = decs ?
                    query.OrderByDescending(x => x.Time).AsQueryable() :
                    query.OrderBy(x => x.Time).AsQueryable();

                query = FilterByDates(query, start, end);

                return query
                    .Select(exp)
                    .Distinct()
                    .Take(amount)
                    .ToList();
            }
        }
    }
}
