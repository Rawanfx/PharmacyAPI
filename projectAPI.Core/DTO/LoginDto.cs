using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Name can't be empty")]
        public string name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can't be empty")]
        public string password { get; set; }
    } 
}
