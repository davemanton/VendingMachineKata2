using System.Collections.Generic;

namespace Application
{
    public class VendingMachine : IVendingMachine
    {
        private readonly ICoinHandler _coinStore;
        private readonly IDispenseProducts _productDispenser;
        private readonly ITransactionRepository _transactionRepository;

        public VendingMachine(ICoinHandler coinStore, IDispenseProducts productDispenser, ITransactionRepository transactionRepository)
        {
            _coinStore = coinStore;
            _productDispenser = productDispenser;
            _transactionRepository = transactionRepository;
        }

        public string CheckDisplay()
        {
            var transaction = _transactionRepository.GetTransaction();

            if (transaction.IsComplete)
                _transactionRepository.ClearTransaction();

            return transaction.GetNotification();
        }

        public void InsertCoin(string pieceOfMetal)
        {
            var transaction = _transactionRepository.GetTransaction();

            _coinStore.InsertCoin(transaction, pieceOfMetal);
        }

        public void CancelTransaction()
        {
            var transaction = _transactionRepository.GetTransaction(); 
            _productDispenser.CancelTransaction(transaction);
            _coinStore.CancelTransaction(transaction);
            _transactionRepository.ClearTransaction();
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            return _coinStore.CheckCoinReturn();
        }

        public void SelectProduct(string sku)
        {
            var transaction = _transactionRepository.GetTransaction();

            _productDispenser.DispenseProduct(transaction, sku);

            if (transaction.IsComplete)
            {
                _coinStore.GiveChange(transaction);
            }
        }

        public IEnumerable<string> CheckDispenser()
        {
            return _productDispenser.CheckProductHopper();
        }
    }
}
