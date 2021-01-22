using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ecdmin.Application.Common.Vos;
using Microsoft.AspNetCore.Mvc;
using Furion.DataValidation;

namespace Ecdmin.Application.Admin.Vos
{
    public class RoleRequest
    {
        public class Get : PageRequest
        {
            [FromQuery(Name = "display_name")]
            public string? DisplayName { get; set; }
        }
        
        public class AddInput
        {
            [Required]
            [DataValidation(ValidationTypes.Ascii)]
            public string Name { get; set; }
            
            [Required]
            public string DisplayName { get; set; }
            
            public string Description { get; set; }
        }
        
        public class UpdateInput: AddInput
        {
            
        }
        
        public class AssignPermissionInput
        {
            [Required]
            public int[] PermissionIds { get; set; }
        }
    }
}