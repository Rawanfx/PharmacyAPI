using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using projectAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name can't be empty")]
        public string name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can't be empty")]
        public string password { get; set; }
        [Compare("password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Confirm Password can't be empty")]
        public string confirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "email can't be empty")]
        public string email { get; set; }
    }
}
