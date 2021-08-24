using System;

namespace Domain
{
    public class Coin
    {
        private Coin(decimal value, string name, CoinType coinType)
        {
            Value = value;
            Name = name;
            CoinType = coinType;
        }

        public decimal Value { get; private init; }
        public string Name { get; private init; }
        public CoinType CoinType { get; set; }

        public static Coin GetCoin(CoinType coinType)
        {
            return coinType switch
            {
                CoinType.Penny => new Coin(0.01m, "penny", CoinType.Penny),
                CoinType.TwoPence => new Coin(0.02m, "two", CoinType.TwoPence),
                CoinType.FivePence => new Coin(0.05m, "five", CoinType.FivePence),
                CoinType.TenPence => new Coin(0.10m, "ten", CoinType.TenPence),
                CoinType.TwentyPence => new Coin(0.20m, "twenty", CoinType.TwentyPence),
                CoinType.FiftyPence => new Coin(0.50m, "fifty", CoinType.FiftyPence),
                CoinType.OnePound => new Coin(1.00m, "pound", CoinType.OnePound),
                CoinType.TwoPounds => new Coin(2.00m, "2pounds", CoinType.TwoPounds),
                _ => throw new ArgumentOutOfRangeException(nameof(coinType), coinType, null)
            };
        }
    }
}
