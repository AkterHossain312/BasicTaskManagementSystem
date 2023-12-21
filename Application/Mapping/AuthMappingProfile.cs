using AutoMapper;
using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.RequestModel;
using Application.ResponseModels;

namespace Application.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, UserResponseModel>()
                .ForMember(x => x.RoleName,
                    opt => opt.MapFrom(x => x.UserRoles.Single().Role.Name))
                .ForMember(x => x.RoleId,
                    opt => opt.MapFrom(x => x.UserRoles.Single().Role.Id))
                .ReverseMap();
        }
    }
}
