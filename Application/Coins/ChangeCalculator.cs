using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class ChangeCalculator : ICalculateChange
    {
        private static readonly IDictionary<decimal, CoinType> _coinTypes = new Dictionary<decimal, CoinType>()
        {
            { 2m, CoinType.TwoPounds },
            { 1m, CoinType.OnePound },
            { 0.50m, CoinType.FiftyPence },
            { 0.20m, CoinType.TwentyPence },
            { 0.10m, CoinType.TenPence },
            { 0.05m, CoinType.FivePence },
            { 0.02m, CoinType.TwoPence },
            { 0.01m, CoinType.Penny },
        };

        public IEnumerable<Coin> Calculate(decimal value)
        {
            while (value > 0m)
            {
                var coinType = GetLargestAvailableCoin(value);

                var coin = Coin.GetCoin(coinType);

                yield return coin;

                value -= coin.Value;
            }
        }

        private CoinType GetLargestAvailableCoin(decimal value)
        {
            return _coinTypes
                  .Select(entry => new { entry.Value, difference = value - entry.Key})
                  .Where(x => x.difference >= 0)
                  .OrderBy(x => x.difference)
                  .First().Value;
        }   
    }
}