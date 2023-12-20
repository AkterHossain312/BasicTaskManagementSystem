using AutoMapper;
using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RequestModel;
using Application.ViewModels;

namespace Application.Mapping
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleRequestModel, Role>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.RoleName))
                .ForMember(x => x.RolePermissions, y =>
                    y.MapFrom(role => role.Permissions.Select(p => new RolePermission()
                    {
                        PermissionId = p.Id,
                        RoleId = role.Id ?? 0
                    }))).ReverseMap();

            CreateMap<RoleModel, Role>().ReverseMap();
        }
    }
}