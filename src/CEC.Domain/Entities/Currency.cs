using CEC.Domain.Common;

namespace CEC.Domain.Entities
{
    public class Currency : AuditEntity, IRootEntity
    {
        public Currency(string name, string code, string symbol)
        {
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