using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using Domain;

namespace Application
{
    public class CoinHandler : ICoinHandler
    {
        private readonly IDetectCoins _coinDetector;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICalculateChange _calculateChange;

        private readonly ICollection<string> _coinReturn;

        public CoinHandler(IDetectCoins coinDetector, 
                           ITransactionRepository transactionRepository,
                           ICalculateChange calculateChange)
        {
            _coinReturn = new List<string>();
            _coinDetector = coinDetector;
            _transactionRepository = transactionRepository;
            _calculateChange = calculateChange;
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

        public void GiveChange(Transaction transaction)
        {
            var change = _calculateChange.Calculate(transaction.ChangeRequired);
            
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