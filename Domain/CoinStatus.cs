using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class CoinStatus
    {
        private readonly List<Coin> _coins;

        public CoinStatus(CoinType coinType, int quantity)
        {
            _coins = new List<Coin>();

            CoinType = coinType;

            for(var i=0; i < quantity; i++)
                _coins.Add(Coin.GetCoin(coinType));
        }

        public CoinType CoinType { get; set; }
        public IReadOnlyCollection<Coin> Coins => _coins;

        public bool IsAvailable => _coins.Any();

        public void AddCoin(Coin coin)
        {
            _coins.Add(coin);
        }

        public Coin GetCoin()
        {
            var coin = _coins.First();
            _coins.Remove(coin);

            return coin;
        }
    }
}