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
            _coinStore.InsertCoin(pieceOfMetal);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            return _coinStore.CheckCoinReturn();
        }

        public void SelectProduct(string sku)
        {
            _productDispenser.DispenseProduct(sku);
        }

        public IEnumerable<string> CheckDispenser()
        {
            return _productDispenser.CheckProductHopper();
        }
    }
}
