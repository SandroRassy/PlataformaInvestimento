namespace Layer.API.Models.DTO
{
    public class PosicaoDto
    {
        public string Symbol { get; set; }
        public string Amount { get; set; }
        public double CurrentPrice { get; set; }

        public PosicaoDto(string symbol, string amount, double currentPrice)
        {
            Symbol = symbol;
            Amount = amount;
            CurrentPrice = currentPrice;
        }
    }
}
