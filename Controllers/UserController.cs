using Microsoft.AspNetCore.Mvc;
using MiApiBackend.Models;
using MiApiBackend.Models.DTOs;
using MiApiBackend.Repositories;

namespace MiApiBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        var existingResult = await _userRepository.GetByEmailAsync(request.Email);
        if (existingResult != null)
            return BadRequest(new { message = "El usuario ya existe" });

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = request.Password, // In production, hash the password!
            Role = "User"
        };
        
        await _userRepository.CreateAsync(user);

        return Ok(new { message = "Usuario registrado correctamente" });
    }
}
