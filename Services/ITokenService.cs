using System.Security.Claims;
using MiApiBackend.Models;

namespace MiApiBackend.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
