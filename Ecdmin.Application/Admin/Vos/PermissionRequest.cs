using System.ComponentModel.DataAnnotations;
using Ecdmin.Application.Common.Vos;
using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Application.Admin.Vos
{
    public class PermissionRequest
    {
        public class Get : PageRequest
        {
            [FromQueryAttribute(Name = "group_id")]
            public int? PermissionGroupId { get; set; }
        }
        
        public class AddInput
        {
            [Required]
            [DataValidation(ValidationTypes.Ascii)]
            public string Name { get; set; }
            
            [Required]
            public string DisplayName { get; set; }
            
            [Required, DataValidation(ValidationTypes.PositiveNumber)]
            public int GroupId { get; set; }
        }
        
        public class UpdateInput : AddInput
        {
            
        }
    }
}