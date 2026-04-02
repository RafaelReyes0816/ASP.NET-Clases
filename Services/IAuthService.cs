using MiApiBackend.Models.DTOs;

namespace MiApiBackend.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<AuthResponse?> RefreshAsync(RefreshRequest request);
    Task LogoutAsync(Guid userId);
}
