using Application.Queries;
using Application.RequestModel;
using Application.ResponseModels;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRoleService
    {
        Task CreateRole(RoleRequestModel model);
        Task<PagedResponse<RoleModel>> GetRoles(BaseQuery model);
        Task<RoleModel> GetRoleById(int id);
        Task Update(RoleRequestModel model);
        Task Delete(int id);
    }
}
