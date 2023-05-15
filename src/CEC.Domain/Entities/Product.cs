using CEC.Domain.Common;
using CEC.Domain.Enums;

namespace CEC.Domain.Entities
{
    public class Product : AuditEntity, IRootEntity
    {
        public Product()
        {
        }

        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Brief { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public EnumProductStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}