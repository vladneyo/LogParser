using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogParser.EDM.Models
{
    public class AccessLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        [Index(IsClustered = false, IsUnique = false)]
        public DateTime Time { get; set; }

        [Required]
        public string Route { get; set; }

        public string QueryParams { get; set; }

        [Required]
        public int RequestStatus { get; set; }

        [Required]
        public int ResponseSize { get; set; }

        public string Geolocation { get; set; }
    }
}
