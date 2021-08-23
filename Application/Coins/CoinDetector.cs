using System.Collections;
using Domain;

namespace Application
{
    public class CoinDetector : IDetectCoins
    {
        public Coin? Detect(string pieceOfMetal)
        {
            var coinType = GetCoinType(pieceOfMetal);

            return coinType == null 
                       ? null 
                       : Coin.GetCoin(coinType.Value);
        }

        private static CoinType? GetCoinType(string pieceOfMetal) => pieceOfMetal.ToLowerInvariant() switch
        {
            "penny" => CoinType.Penny,
            "two" => CoinType.TwoPence,
            "five" => CoinType.FivePence,
            "ten" => CoinType.TenPence,
            "twenty" => CoinType.TwentyPence,
            "fifty" => CoinType.FiftyPence,
            "pound" => CoinType.OnePound,
            "2pounds" => CoinType.TwoPounds,
            _ => null
        };
    }
}