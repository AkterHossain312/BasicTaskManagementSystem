using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging
{
    public class DbContextService : IDbContextService
    {
        private FrameworkDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public DbContextService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public FrameworkDbContext GetContext()
        {
            if (_context == null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
                    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConnectionStrings:DefaultConnectionString"));
                    _context = new FrameworkDbContext(optionsBuilder.Options);
                }
            }

            return _context;
        }
    }
}