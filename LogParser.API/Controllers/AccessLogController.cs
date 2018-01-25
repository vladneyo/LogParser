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

        [HttpGet]
        [Route("")]
        public List<AccessLogDto> Get()
        {
            return _accessLogLogic.GetAll();
        }

        [HttpPost]
        [Route("")]
        public List<AccessLogDto> Post([FromBody]List<AccessLogDto> accesslogs)
        {
            return _accessLogLogic.CreateBulk(accesslogs.Where(x => x.Time != "").ToList());
        }
    }
}
