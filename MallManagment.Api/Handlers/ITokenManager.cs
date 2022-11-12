using MallManagment.Models.Dtos;
using MallManagment.Models.Entities;
using System.Security.Claims;

namespace MallManagment.Api.Handlers
{
    public interface ITokenManager
    {
        Task<string> GenerateToken(AuthDto admin); 
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
