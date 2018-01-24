using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using LogParser.EDM.Models;

namespace LogParser.EDM
{
    public class ParserContext : DbContext
    {
        public ParserContext() : base("ParserDbConnection")
        {
            
        }

        public DbSet<AccessLog> AccessLogs { get; set; }
    }
}
