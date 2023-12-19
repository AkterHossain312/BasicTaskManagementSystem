using Framework.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Logging
{
    public class FrameworkDbContext : DbContext
    {
        public FrameworkDbContext(DbContextOptions<FrameworkDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationLog> ApplicationLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine);
        }        
    }
}
