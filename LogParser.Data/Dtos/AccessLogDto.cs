using System;
using System.ComponentModel.DataAnnotations;

namespace LogParser.Data.Dtos
{
    public class AccessLogDto
    {
        public int Id { get; set; }

        public string Host { get; set; }

        public string Time { get; set; }

        public string Route { get; set; }

        public string QueryParams { get; set; }

        public int RequestStatus { get; set; }

        public int ResponseSize { get; set; }
    }
}
