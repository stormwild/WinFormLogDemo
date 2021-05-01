using Log4NetDemo.Persistence;
using Microsoft.Extensions.Logging;
using System;

namespace Log4NetDemo.BusinessLayer
{
    public class MainService : IMainService
    {
        private readonly ILogger<MainService> _logger;
        private readonly IDbContext _context;

        public MainService(ILogger<MainService> logger, IDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void DoTask()
        {
            _logger.LogInformation("DoTask Started");
            _context.Add();
        }
    }
}
