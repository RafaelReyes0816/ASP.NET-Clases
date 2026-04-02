using System.ComponentModel.DataAnnotations;

namespace MiApiBackend.Models;

public class RefreshToken
{
    [Key]
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedByIp { get; set; } = string.Empty;

    public User? User { get; set; }
}
