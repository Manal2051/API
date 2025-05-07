using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestToken.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string? OTP {  get; set; }
        public DateTime? OTPExpiry { get; set; } = DateTime.UtcNow;
        public bool IsConfirmed { get; set; } = false;
        public ICollection<RefreshToken>  RefreshTokens { get; set; } = new List<RefreshToken>();

    }
}
