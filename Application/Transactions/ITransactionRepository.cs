using Domain;

namespace Application
{
    public interface ITransactionRepository
    {
        Transaction GetTransaction();
        void ClearTransaction();
    }
}