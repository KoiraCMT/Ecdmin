using Furion.FriendlyException;

namespace Ecdmin.Application.Common
{
    public class ExceptionService
    {
        public static void NotFound()
        {
            throw Oops.Oh("Not Found").StatusCode(404);
        }
    }
}