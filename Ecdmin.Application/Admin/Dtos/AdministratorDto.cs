using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Ecdmin.Application.Admin.Dtos
{
    public class AdministratorDto
    {
        public class List
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string? Avatar { get; set; }
            
            public string CreatedTime { get; set; }
            public string? UpdatedTime { get; set; }

            public List<int> RoleIds { get; set; }

            public string? Roles { get; set; }
        }
    }
}