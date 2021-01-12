using System.Text.Json.Serialization;

namespace Ecdmin.Application.Admin.Dtos
{
    public class AdminUserDto
    {
        public class List
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string? Avatar { get; set; }
            
            public string CreatedTime { get; set; }
            public string? UpdatedTime { get; set; }
        }
    }
}