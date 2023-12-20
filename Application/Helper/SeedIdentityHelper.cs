using Domain.Helpers;
using Domain.Models.Identity;
using Infrastructure;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class SeedIdentityHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPermissionRepository _permissionRepository;
        private readonly TaskManagementDbContext _context;

        public SeedIdentityHelper(UserManager<User> userManager, RoleManager<Role> roleManager, IPermissionRepository permissionRepository, TaskManagementDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionRepository = permissionRepository;
            _context = context;
        }
        public async Task InsertRoleWithPermissions(SeedRolesModel seedRole)
        {
            var rolePermissions = new List<RolePermission>();
            seedRole.Permissions.ForEach(seedRolePermissionId =>
            {
                rolePermissions.Add(new RolePermission()
                {
                    PermissionId = seedRolePermissionId,
                    RoleId = seedRole.Id
                });
            });

            await _roleManager.CreateAsync(new Role()
            {
                Name = seedRole.Name,
                RolePermissions = rolePermissions
            });
        }

        public async Task UpdatePermissionsToExistingRole(SeedRolesModel seedRole, Role existingRole)
        {
            var newPermissionId = seedRole.Permissions.Where(seedPermissionId => existingRole.RolePermissions
                .All(y => y.PermissionId != seedPermissionId))
                .ToList();

            foreach (var permissionId in newPermissionId)
            {
                existingRole.RolePermissions.Add(new RolePermission()
                {
                    RoleId = existingRole.Id,
                    PermissionId = permissionId
                });
                await _roleManager.UpdateAsync(existingRole);
            }
        }

        public void DescriptionUpdateOfExistingPermission(List<Permission> seedPermissions, IReadOnlyList<Permission> existingPermissions)
        {
            seedPermissions.ForEach(seedPermission =>
            {
                var permission = existingPermissions.FirstOrDefault(x => x.Id == seedPermission.Id);
                if (permission != null)
                {
                    permission.Description = seedPermission.Description;
                    _permissionRepository.UpdateAsync(permission);
                }
            });
        }
    }
}
