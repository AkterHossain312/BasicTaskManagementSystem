using Application.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IPermissionService
    {
        Task<List<PermissionResponseModel>> GetAllPermissions();
        Task<List<string>> GetPermissionsByUserId(int userId);
    }
}