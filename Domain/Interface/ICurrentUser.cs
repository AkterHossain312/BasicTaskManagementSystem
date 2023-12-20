using System.Security.Claims;

namespace Domain.Interface;


public interface ICurrentUser
    {
        int UserId { get; }
        string Role { get; }
        List<Claim> Claims { get; set; }
        void SetClaims(IEnumerable<Claim> claims);
    }
