using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace school.Data.Entites.identity
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }

        public string? Countery { get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }




    }
}
