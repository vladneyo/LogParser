using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LogParser.Data.Converters;
using LogParser.Data.Dtos;

namespace LogParser.API.Controllers
{
    [RoutePrefix("api/log/access")]
    public class AccessLogController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "get api/log/access";
        }

        [HttpPost]
        [Route("")]
        public void Post([FromBody]List<AccessLogDto> accesslog)
        {
            // how to convert string datetime from access log to normal datetime object
            var a = new AccessLogDatetimeConverter().Convert(accesslog[0].Time);
        }
    }
}
