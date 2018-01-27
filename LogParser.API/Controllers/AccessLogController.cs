using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LogParser.Business.Contracts;
using LogParser.Data.Dtos;

namespace LogParser.API.Controllers
{
    [RoutePrefix("api/log/access")]
    public class AccessLogController : ApiController
    {
        private readonly IAccessLogLogic _accessLogLogic;

        public AccessLogController(IAccessLogLogic accessLogLogic)
        {
            _accessLogLogic = accessLogLogic;
        }

        // /api/log/access
        [HttpGet]
        [Route("")]
        public List<AccessLogDto> Get()
        {
            return _accessLogLogic.GetAll();
        }

        // /api/log/access?offset=1&start=03/09/2004
        [HttpGet]
        [Route("")]
        public List<AccessLogDto> Get([FromUri]int offset, [FromUri]DateTime? start, [FromUri]DateTime? end, [FromUri]int? limit)
        {
            return _accessLogLogic.GetAll(offset, start, end, limit.GetValueOrDefault());
        }

        // /api/log/access/hosts?amount=10&start=03/09/2004&end=&decs=true
        [HttpGet]
        [Route("hosts")]
        public List<string> GetHosts([FromUri]int? amount, [FromUri]DateTime? start, [FromUri]DateTime? end, [FromUri]bool? decs)
        {
            return _accessLogLogic.GetTopHosts(amount.GetValueOrDefault(), start, end, decs.GetValueOrDefault());
        }

        // /api/log/access/routes?amount=10&start=03/09/2004&end=&decs=true
        [HttpGet]
        [Route("routes")]
        public List<string> GetRoutes([FromUri]int? amount, [FromUri]DateTime? start, [FromUri]DateTime? end, [FromUri]bool? decs)
        {
            return _accessLogLogic.GetTopRoutes(amount.GetValueOrDefault(), start, end, decs.GetValueOrDefault());
        }

        [HttpPost]
        [Route("")]
        public List<AccessLogDto> Post([FromBody]List<AccessLogDto> accesslogs)
        {
            return _accessLogLogic.CreateBulk(accesslogs.Where(x => x.Time != "").ToList());
        }
    }
}
