using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.ObjectMapper;
using Mapster;

namespace Ecdmin.Application.Admin.Dtos
{
    public class Mapper : IObjectMapper
    {

        private TypeAdapterConfig _config;
        public void Register(TypeAdapterConfig config)
        {
            _config = config;
            AdminUser();
            Permission();
            Role();
        }

        private void Role()
        {
            _config.ForType<RoleRequest.AddInput, Role>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.DisplayName, src => src.DisplayName)
                .Map(dest => dest.Description, src => src.Description);
            _config.ForType<Role, RoleDto.List>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.DisplayName, src => src.DisplayName)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.CreatedTime, src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.UpdatedTime, src => src.UpdatedTime != null ? src.UpdatedTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null);
        }

        private void AdminUser()
        {
            _config.ForType<AuthRequest.LoginInput, AdminUser>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Password, src => src.Password);
            _config.ForType<AdminUser, AdminUserDto.List>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Avatar, src => src.Avatar)
                .Map(dest => dest.CreatedTime, src => src.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"))
                .Map(dest => dest.UpdatedTime, src => src.UpdatedTime != null ? src.UpdatedTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null);
        }

        private void Permission()
        {
            _config.ForType<PermissionRequest.AddInput, Permission>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.DisplayName, src => src.DisplayName)
                .Map(dest => dest.PermissionGroupId, src => src.GroupId);
        }
    }
}
