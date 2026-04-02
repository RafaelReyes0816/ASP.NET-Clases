namespace MiApiBackend.Models.DTOs;

public record UpdateRequest(string Email, string? Password, string? Role);
