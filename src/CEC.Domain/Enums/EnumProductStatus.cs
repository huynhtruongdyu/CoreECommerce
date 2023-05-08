using System.ComponentModel;

namespace CEC.Domain.Enums
{
    public enum EnumProductStatus
    {
        [Description("Draft")]
        Draft,

        [Description("Active")]
        Active,

        [Description("Archived")]
        Archived
    }
}