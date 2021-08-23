using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class CoinHandler : ICoinHandler
    {
        private readonly IDetectCoins _coinDetector;
        private readonly ITransactionRepository _transactionRepository;

        private readonly ICollection<string> _coinReturn;

        public CoinHandler(IDetectCoins coinDetector, 
                           ITransactionRepository transactionRepository)
        {
            _coinReturn = new List<string>();
            _coinDetector = coinDetector;
            _transactionRepository = transactionRepository;
        }

        public void InsertCoin(string pieceOfMetal)
        {
            var coin = _coinDetector.Detect(pieceOfMetal);

            if (coin != null)
            {
                var transaction = _transactionRepository.GetTransaction();
                transaction.AddCoin(coin);
            }
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