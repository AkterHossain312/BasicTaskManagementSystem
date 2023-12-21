using Domain.Models.Identity;

namespace Application.Interface
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<List<string>> GetPermissionsByUserId(int userId);
    }
}
