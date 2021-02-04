using System.Collections.Generic;
using System.Linq;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.LinqBuilder;
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
            Administrator();
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
            _config.ForType<Role, RoleDto.WithPermission>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.DisplayName, src => src.DisplayName)
                .Map(dest => dest.Permissions, src => src.RolePermissions.Select(t => t.Permission));
        }

        private void Administrator()
        {
            _config.ForType<AuthRequest.LoginInput, Administrator>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Password, src => src.Password);
            _config.ForType<Administrator, AdministratorDto.List>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Avatar, src => src.Avatar)
                .Map(dest => dest.RoleIds, src => src.AdministratorRoles.IsNullOrEmpty() ? new List<int>() : src.AdministratorRoles.Select(t => t.RoleId))
                .Map(dest => dest.Roles, src => src.AdministratorRoles.IsNullOrEmpty() ? null : string.Join(",", src.AdministratorRoles.Select(t => t.Role.DisplayName)) )
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
