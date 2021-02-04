namespace Ecdmin.Application.Admin.Dtos
{
    public class LabelValueDto<TK, TV>
    {
        public TK Label { get; set; }
        public TV Value { get; set; }
    }
}