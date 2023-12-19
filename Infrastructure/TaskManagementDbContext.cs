using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class TaskManagementDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TaskManagementDbContext()
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseSqlServer("Server=sql-msfsax01-01.database.windows.net;Initial Catalog=sqldb-msfsax01-266-smt-prd;User ID=smt_prd_usr;Password=R7pVWXH4yHhU3TPy;MultipleActiveResultSets=True;Connection Timeout=600");
        //    base.OnConfiguring(builder);
        //}

        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //public virtual DbSet<StakeholderInfo> StakeholderInfo { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
            // Don't call base.OnModelCreating(modelBuilder);
            // It's not required: https://stackoverflow.com/questions/39576176/is-base-onmodelcreatingmodelbuilder-necessary
            // and in this particular case creates problems in migrations.

            // TODO: Transfer these to Configurations folder in separate classes

            //OnModelCreatingPartial(modelBuilder);


            //modelBuilder.Entity<UserInfo>().HasData(
            //    new UserInfo { Id = 1, Name = "Khondokar, Anm Robiul Hassan", Email = "DHAKHONDAN@CORP.JTI.COM", UserRole = Domain.Enumerations.UserRole.Admin },
            //    new UserInfo { Id = 2, Name = "Robin, Nazmul Hossain", Email = "CSTROBINN@CORP.JTI.COM", UserRole = Domain.Enumerations.UserRole.Admin },
            //    new UserInfo { Id = 3, Name = "Mahbub, Abdullah-Al", Email = "CSTMAHBUA@CORP.JTI.COM", UserRole = Domain.Enumerations.UserRole.Admin },
            //    new UserInfo { Id = 4, Name = "Khan, Abdul Kaium", Email = "CSTKHANA@CORP.JTI.COM", UserRole = Domain.Enumerations.UserRole.Admin },
            //    new UserInfo { Id = 5, Name = "Hossain, Akter", Email = "CSTHOSSAAK@CORP.JTI.COM", UserRole = Domain.Enumerations.UserRole.Admin }

            //    );         


        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

            foreach (var item in ChangeTracker.Entries<BaseModel>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedBy = userId;
                        item.Entity.DateCreated = DateTime.Now;

                        break;
                    case EntityState.Modified:
                        item.Entity.UpdatedBy = userId;
                        item.Entity.LastUpdated = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
