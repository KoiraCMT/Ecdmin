using Furion.DatabaseAccessor;

namespace Ecdmin.Core.Entities.Admin
{
    public class AdministratorRole : Entity
    {
        public int AdministratorId { get; set; }

        public int RoleId { get; set; }

        public virtual Administrator Administrator { get; set; }
    }
}