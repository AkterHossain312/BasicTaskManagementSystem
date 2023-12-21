using Application.Interface;
using Application.ResponseModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<List<string>> GetPermissionsByUserId(int userId)
        {
            return await _permissionRepository.GetPermissionsByUserId(userId);
        }

        public async Task<List<PermissionResponseModel>> GetAllPermissions()
        {
            var result = await _permissionRepository.GetAllList();
            return _mapper.Map<List<PermissionResponseModel>>(result);
        }
    }
}