using System;

namespace Domain
{
    public class ProductStatus
    {
        public ProductStatus(string sku, string product, decimal cost)
        {
            Sku = sku;
            Product = product;
            Cost = cost;
        }

        public string Sku { get; private init; }
        public string Product { get; private init; }
        public decimal Cost { get; private init; }
    }

    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; private init; }
    }

    public class Coin
    {
        private Coin(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private init; }

        public static Coin GetCoin(CoinType coinType)
        {
            return coinType switch
            {
                CoinType.Penny => new Coin(0.01m),
                CoinType.TwoPence => new Coin(0.02m),
                CoinType.FivePence => new Coin(0.05m),
                CoinType.TenPence => new Coin(0.10m),
                CoinType.TwentyPence => new Coin(0.20m),
                CoinType.FiftyPence => new Coin(0.50m),
                CoinType.OnePound => new Coin(1.00m),
                CoinType.TwoPounds => new Coin(2.00m),
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
