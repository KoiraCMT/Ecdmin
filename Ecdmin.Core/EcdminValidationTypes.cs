using Furion.DataValidation;

namespace Ecdmin.Core
{
    [ValidationType]
    public enum EcdminValidationTypes
    {
        [ValidationItemMetadata(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$", "必须须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间")]
        StrongPassword,
    }
}