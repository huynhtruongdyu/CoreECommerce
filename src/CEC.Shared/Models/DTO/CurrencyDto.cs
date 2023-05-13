namespace CEC.Shared.Models.DTO
{
    public class CurrencyDto
    {
        public CurrencyDto(long id, string name, string code, string symbol)
        {
            Id = id;
            Name = name;
            Code = code;
            Symbol = symbol;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}