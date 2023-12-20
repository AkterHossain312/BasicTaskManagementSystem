using Application.Constant;
using Application.Interface;
using Application.Queries;
using Application.RequestModel;
using Application.ResponseModels;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models.Identity;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task CreateRole(RoleRequestModel model)
        {
            var role = _mapper.Map<Role>(model);
            var existingPermissionOfRole = await _roleManager.Roles.Where(x => x.Name == role.Name)
                                                            .Include(x => x.RolePermissions).FirstOrDefaultAsync();

            if (existingPermissionOfRole != null)
            {
                existingPermissionOfRole.Name = role.Name;
                existingPermissionOfRole.RolePermissions.Clear();
                role.RolePermissions.ToList().ForEach(rp => existingPermissionOfRole.RolePermissions.Add(rp));
            }

            var result = await _roleManager.RoleExistsAsync(role.Name) ?
                                                await _roleManager.UpdateAsync(existingPermissionOfRole) :
                                                await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                throw new DomainException(result.Errors.First().Description);
        }

        public async Task<PagedResponse<RoleModel>> GetRoles(BaseQuery model)
        {
            var data = _roleManager.Roles.AsQueryable()
                                            .ProjectTo<RoleModel>(_mapper.ConfigurationProvider)
                                            .AsNoTracking();
            return new PagedResponse<RoleModel>(data, Pagination.FromQuery(data.Count(), model.PageNo, model.PageSize));
        }

        public async Task<RoleModel> GetRoleById(int id)
        {
            return await _roleManager.Roles.Where(x => x.Id == id).AsQueryable()
                                    .Include(x => x.RolePermissions)
                                    .ProjectTo<RoleModel>(_mapper.ConfigurationProvider).AsNoTracking()
                                    .FirstOrDefaultAsync();
        }

        public Task Update(RoleRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            var existingRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRole == null)
                throw new NullReferenceException(MessageConstants.RoleNotFound);

            await _roleManager.DeleteAsync(existingRole);
        }
    }
}
