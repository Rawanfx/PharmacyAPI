using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.DTO
{
    public class AuthenticationRespose
    {
        public string name { get; set; }
        public string email { get; set; }
       public string token { get; set; }
        public string error { get; set; }
    }
}
