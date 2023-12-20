using Domain.Models.Identity;
using Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(TaskManagementDbContext context) : base(context) { }

        public async Task<List<string>> GetPermissionsByUserId(int userId)
        {
            return await _context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .Where(x => x.Role.UserRoles.Any(y => y.UserId == userId))
                .Select(x => x.Permission.PermissionName)
                .ToListAsync();
        }
    }
}