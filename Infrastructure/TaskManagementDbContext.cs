using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Interface;

namespace Infrastructure
{
    public class TaskManagementDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUser _currentUser;

        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor = null)
            : base(options)
        {
            _currentUser = currentUser;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementDbContext).Assembly);
            

        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault()?.Value);

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
