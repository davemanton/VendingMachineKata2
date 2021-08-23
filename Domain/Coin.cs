using System;

namespace Domain
{
    public class Coin
    {
        private Coin(decimal value, string name)
        {
            Value = value;
            Name = name;
        }

        public decimal Value { get; private init; }
        public string Name { get; private init; }

        public static Coin GetCoin(CoinType coinType)
        {
            return coinType switch
            {
                CoinType.Penny => new Coin(0.01m, "penny"),
                CoinType.TwoPence => new Coin(0.02m, "two"),
                CoinType.FivePence => new Coin(0.05m, "five"),
                CoinType.TenPence => new Coin(0.10m, "ten"),
                CoinType.TwentyPence => new Coin(0.20m, "twenty"),
                CoinType.FiftyPence => new Coin(0.50m, "fifty"),
                CoinType.OnePound => new Coin(1.00m, "pound"),
                CoinType.TwoPounds => new Coin(2.00m, "2pounds"),
                _ => throw new ArgumentOutOfRangeException(nameof(coinType), coinType, null)
            };
        }
    }

    public enum CoinType
    {
        Penny,
        TwoPence,
        FivePence,
        TenPence,
        TwentyPence,
        FiftyPence,
        OnePound,
        TwoPounds
    }
}
