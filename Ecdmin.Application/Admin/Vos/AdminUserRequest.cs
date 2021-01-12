using System.ComponentModel.DataAnnotations;
using Ecdmin.Application.Common.Vos;
using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Application.Admin.Vos
{
    public class AdminUserRequest
    {
        public class Base
        {
            [Required]
            [MinLength(2)]
            public string Name { get; set; }
            [DataValidation(ValidationTypes.Image)] 
            public string Avatar { get; set; }
        }
        public class AddInput : Base
        {
            [Required]
            [MinLength(4)]
            public string Username { get; set; }
            
            [Required]
            [MinLength(6)] 
            public string Password { get; set; }
        }
        
        public class EditInput : Base
        {
            [MinLength(6)]
            public string Password { get; set; }
        }
        
        public class Get : PageRequest
        {
            public string Name { get; set; } = null;
        }
    }
}