using CEC.Domain.Common;

namespace CEC.Domain.Entities
{
    public class Currency : AuditEntity, IRootEntity
    {
        public Currency(long id, string name, string code, string symbol)
        {
            Id = id;
            Name = name;
            Code = code;
            Symbol = symbol;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool IsDeleted { get; set; }
    }
}