using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Services.Models.Shared
{
    public class PosicaoShared
    {
        public string Symbol { get; set; }
        public string Amount { get; set; }
        public double CurrentPrice { get; set; }
        public PosicaoShared(string symbol, string amount, double currentPrice)
        {
            Symbol = symbol;
            Amount = amount;
            CurrentPrice = currentPrice;
        }
    }
}
