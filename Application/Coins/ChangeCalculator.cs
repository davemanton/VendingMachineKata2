using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
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

        public bool TryCalculate(ICollection<CoinStatus> availableCoins, decimal changeRequired, out ICollection<Coin> change)
        {
            change = new HashSet<Coin>();
            
            Coin? nextCoin;
            var remainingChangeRequired = changeRequired;
            
            do
            {
                nextCoin = GetLargestAvailableCoin(availableCoins, remainingChangeRequired);

                if (nextCoin == null)
                    continue;

                change.Add(nextCoin);
                remainingChangeRequired -= nextCoin.Value;

            } while (remainingChangeRequired > 0 || nextCoin != null);

            var isSuccess = change.Sum(x => x.Value) == changeRequired;

            if(!isSuccess)
                ReturnCoins(change, availableCoins);

            return isSuccess;
        }

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

        private Coin? GetLargestAvailableCoin(ICollection<CoinStatus> availableCoins, decimal value)
        {
            var bestMatch = availableCoins.Where(x => x.IsAvailable)
                                          .Select(coinStatus => new { coinStatus, difference = value - coinStatus.Coins.First().Value })
                                          .Where(x => x.difference >= 0)
                                          .OrderBy(x => x.difference)
                                          .FirstOrDefault()?.coinStatus;

            return bestMatch?.GetCoin();
        }

        private void ReturnCoins(IEnumerable<Coin> coinsToReturn, ICollection<CoinStatus> coinStore)
        {
            foreach (var coin in coinsToReturn)
            {
                coinStore.Single(x => x.CoinType == coin.CoinType).AddCoin(coin);
            }
        }
    }
}