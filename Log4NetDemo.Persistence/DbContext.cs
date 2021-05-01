using Microsoft.Extensions.Logging;
using System;

namespace Log4NetDemo.Persistence
{
    public class DbContext : IDbContext
    {
        private readonly ILogger<DbContext> _logger;

        public DbContext(ILogger<DbContext> logger)
        {
            _logger = logger;
        }

        public void Add()
        {
            _logger.LogInformation("Adding Entity");
        }
    }
}
