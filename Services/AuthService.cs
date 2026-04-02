using System.Security.Claims;
using Microsoft.Extensions.Options;
using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Repositories;
using MiApiBackend.Settings;

namespace MiApiBackend.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null || user.PasswordHash != request.Password) // En producción usar BCrypt o similar
            return null;

        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpiryDays),
            CreatedByIp = "127.0.0.1" // Simplificado
        });

        return new AuthResponse(token, refreshToken);
    }

    public async Task<AuthResponse?> RefreshAsync(RefreshRequest request)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
        if (principal == null) return null;

        var userIdString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return null;

        var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        if (storedRefreshToken == null || storedRefreshToken.UserId != userId || storedRefreshToken.ExpiryDate < DateTime.UtcNow)
        {
            return null;
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return null;

        await _refreshTokenRepository.DeleteAsync(request.RefreshToken);

        var newToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.CreateAsync(new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpiryDays),
            CreatedByIp = storedRefreshToken.CreatedByIp
        });

        return new AuthResponse(newToken, newRefreshToken);
    }

    public async Task LogoutAsync(Guid userId)
    {
        await _refreshTokenRepository.DeleteAllByUserIdAsync(userId);
    }
}
