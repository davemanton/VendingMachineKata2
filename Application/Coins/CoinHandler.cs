using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class CoinHandler : ICoinHandler
    {
        private readonly IDetectCoins _coinDetector;
        private readonly ICalculateChange _calculateChange;

        private readonly ICollection<string> _coinReturn;
        private readonly ICollection<CoinStatus> _availableCoins;

        public CoinHandler(IDetectCoins coinDetector, 
                           ICalculateChange calculateChange,
                           ICollection<CoinStatus> availableCoins)
        {
            _coinReturn = new List<string>();
            _coinDetector = coinDetector;
            _calculateChange = calculateChange;
            _availableCoins = availableCoins;
        }

        public void InsertCoin(Transaction transaction, string pieceOfMetal)
        {
            var coin = _coinDetector.Detect(pieceOfMetal);

            if (coin != null)
            {
                transaction.AddCoin(coin);

                _availableCoins.Single(x => x.CoinType == coin.CoinType).AddCoin(coin);
            }
            else
                _coinReturn.Add(pieceOfMetal);
        }

        public void CancelTransaction(Transaction transaction)
        {
            foreach (var coin in transaction.Coins)
            {
                var coinStatus = _availableCoins.Single(x => x.CoinType == coin.CoinType);
                var coinToReturn = coinStatus.GetCoin();
                _coinReturn.Add(coinToReturn.Name);
            }
        }

        public void GiveChange(Transaction transaction)
        {
            _calculateChange.TryCalculate(_availableCoins, transaction.ChangeRequired, out var change);
            
            foreach(var coin in change)
                _coinReturn.Add(coin.Name);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            var returnedCoins = _coinReturn.ToList();

            _coinReturn.Clear();

            return returnedCoins;
        }
    }
}