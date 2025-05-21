using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.Domain.IdentityEntities
{
    public class ApplicationUser:IdentityUser
    {
        public bool? gender { get; set; }
        public DateTime? birthdate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? phone { get; set; }
    }
}
