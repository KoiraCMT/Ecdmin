using Furion.FriendlyException;

namespace Ecdmin.Core
{
    [ErrorCodeType]
    public enum ErrorCode
    {
        [ErrorCodeItemMetadata("id为{0}的数据不存在")]
        NOT_FOUND,
    }
}