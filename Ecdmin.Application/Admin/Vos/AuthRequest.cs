using System.ComponentModel.DataAnnotations;

namespace Ecdmin.Application.Admin.Vos
{
    public class AuthRequest
    {
        public class LoginInput
        {
            [Required]
            [MinLength(4)]
            public string Username { get; set; }

            // [JsonProperty("password")]
            [Required]
            [MinLength(6)]
            public string Password { get; set; }
        }
    }
}