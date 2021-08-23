using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class VendingMachine : IVendingMachine
    {
        private readonly IDetectCoins _coinDetector;
        private readonly ICollection<Coin> _coins;
        private readonly ICollection<string> _coinReturn;

        public VendingMachine(IDetectCoins coinDetector)
        {
            _coins = new List<Coin>();
            _coinReturn = new List<string>();

            _coinDetector = coinDetector;
        }

        public string CheckDisplay()
        {
            if (!_coins.Any())
                return "INSERT COINS";

            var total = _coins.Sum(x => x.Value);
            
            return $"£{total}";
        }

        public void InsertCoin(string pieceOfMetal)
        {
            var coin = _coinDetector.Detect(pieceOfMetal);

            if (coin != null)
                _coins.Add(coin);
            else 
                _coinReturn.Add(pieceOfMetal);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            return _coinReturn;
        }
    }
}
