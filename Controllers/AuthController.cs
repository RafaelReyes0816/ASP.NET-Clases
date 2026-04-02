using Microsoft.AspNetCore.Mvc;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Services;

namespace MiApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(new { message = "Credenciales inválidas" });

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var result = await _authService.RefreshAsync(request);
        if (result == null)
            return Unauthorized(new { message = "Token de refresco inválido o expirado" });

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] Guid userId)
    {
        await _authService.LogoutAsync(userId);
        return Ok(new { message = "Sesión cerrada correctamente" });
    }
}
