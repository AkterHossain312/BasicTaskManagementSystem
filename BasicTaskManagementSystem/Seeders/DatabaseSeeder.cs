using Application.Helper;
using AutoMapper;
using Domain.Helpers;
using Domain.Models.Identity;
using Infrastructure;
using Infrastructure.Constant;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebApi.Seeders
{
    public class DatabaseSeeder : BaseSeeder
    {
        private readonly SeedDataFilesConfiguration _fileConfig;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;
        private readonly TaskManagementDbContext _context;
        private readonly SeedIdentityHelper _seedIdentityHelper;

        public DatabaseSeeder(IWebHostEnvironment hostEnvironment, IOptions<SeedDataFilesConfiguration> fileConfiguration,
            UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IPermissionRepository permissionRepository, TaskManagementDbContext context, SeedIdentityHelper seedIdentityHelper)
            : base(hostEnvironment, fileConfiguration, context)
        {
            _hostEnvironment = hostEnvironment;
            _fileConfig = fileConfiguration.Value;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
            _context = context;
            _seedIdentityHelper = seedIdentityHelper;
        }

        public async Task Seed()
        {
            await SeedPermissions();
            await SeedRolesWithPermissions();
            await SeedUsersWithRoles();
        }

        public async Task SeedPermissions()
        {
            var seedPermissions = ReadJsonData<Permission>(_fileConfig.PermissionsFilename);
            var existingPermissions = await _permissionRepository.GetAllList();
            var newIds = seedPermissions.Select(x => x.Id).Except(existingPermissions.Select(x => x.Id)).ToList();

            await _permissionRepository.InsertRange(seedPermissions.Where(x => newIds.Contains(x.Id)));
            _seedIdentityHelper.DescriptionUpdateOfExistingPermission(seedPermissions, existingPermissions);

            await WriteToDb(TableNames.Permissions);
        }

        private async Task SeedUsersWithRoles()
        {
            var seedUsers = ReadJsonData<SeedUsersModel>(_fileConfig.UsersFileName);
            var users = _mapper.Map<List<User>>(seedUsers);
            foreach (var user in users)
            {
                var seedUser = seedUsers.FirstOrDefault(su => su.Id == user.Id);
                user.Id = 0;
                var result = await _userManager.CreateAsync(user, AppConstants.DefaultPassword);
                if (result.Succeeded)
                {
                    var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == seedUser.RoleId);
                    _userManager.AddToRoleAsync(user, role?.Name).Wait();
                }
            }
        }

        public async Task SeedRolesWithPermissions()
        {
            var seedRoles = ReadJsonData<SeedRolesModel>(_fileConfig.RolesFileName);
            var existingRoles = await _roleManager.Roles.Include(x => x.RolePermissions).ToListAsync();

            foreach (var seedRole in seedRoles)
            {
                var existingRole = existingRoles.FirstOrDefault(x => x.Id == seedRole.Id);
                if (existingRole != null)
                {
                    await _seedIdentityHelper.UpdatePermissionsToExistingRole(seedRole, existingRole);
                }
                else
                {
                    await _seedIdentityHelper.InsertRoleWithPermissions(seedRole);
                }
            }
        }
    }
}
