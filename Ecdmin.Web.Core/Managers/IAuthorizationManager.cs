namespace Ecdmin.Web.Core.Managers
{
    public interface IAuthorizationManager
    {
        int GetUserId();

        bool CheckPermission(string resourceId);
    }
}