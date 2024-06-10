using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserApi.Models;

public class User : BaseEntity
{
    [MaxLength(50)]
    public required string Username { get; set; }
    
    [MaxLength(128)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    [JsonIgnore]
    public byte[] PasswordHash { get; set; } = new byte[64];
    
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; } = new byte[128];
}