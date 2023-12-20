using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<List<string>> GetPermissionsByUserId(int userId);
    }
}
