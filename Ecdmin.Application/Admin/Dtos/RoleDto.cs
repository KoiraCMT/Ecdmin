namespace Ecdmin.Application.Admin.Dtos
{
    public class RoleDto
    {
        public class List
        {
            public int Id { get; set; }

            public string Name { get; set; }
            
            public string DisplayName { get; set; }
            
            public string? Description { get; set; }
            
            public string CreatedTime { get; set; }
            
            public string? UpdatedTime { get; set; } 
        }
    }
}