using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class CoinStore : IStoreCoins
    {
        private readonly ICollection<Coin> _coins;
        private readonly ICollection<string> _coinReturn;

        private readonly IDetectCoins _coinDetector;

        private bool _isExpectingCoins;

        private CoinStore()
        {
            _coins = new List<Coin>();
            _coinReturn = new List<string>();
        }

        public CoinStore(IDetectCoins coinDetector) 
            : this()
        {
            _coinDetector = coinDetector;
        }

        public bool IsExpectingCoins => _coins.Sum(x => x.Value) == 0 || _isExpectingCoins;

        public void Empty() => _coins.Clear();
        public void SetExpectingCoins(bool isExpectingCoins) => _isExpectingCoins = true;

        public decimal GetTotal()
        {
            return _coins.Sum(x => x.Value);
        }

        public void AddCoin(string pieceOfMetal)
        {
            var coin = _coinDetector.Detect(pieceOfMetal);

            if (coin != null)
                _coins.Add(coin);
            else
                _coinReturn.Add(pieceOfMetal);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            var returnedCoins = _coinReturn.ToList();

            _coinReturn.Clear();

            return returnedCoins;
        }
    }
}