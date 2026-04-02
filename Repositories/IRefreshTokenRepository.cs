using MiApiBackend.Models;

namespace MiApiBackend.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task CreateAsync(RefreshToken refreshToken);
    Task DeleteAsync(string token);
    Task DeleteAllByUserIdAsync(Guid userId);
}
