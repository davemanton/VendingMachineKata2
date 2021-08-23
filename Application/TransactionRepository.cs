using Domain;

namespace Application
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction();
        void ClearTransaction();
    }

    public class TransactionRepository : ITransactionRepository
    {
        private Transaction? _transaction;

        public Transaction GetTransaction()
        {
            return _transaction ??= new Transaction();
        }

        public void ClearTransaction()
        {
            _transaction = null;
        }
    }
}