using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface ICoinHandler
    {
        void InsertCoin(Transaction transaction, string pieceOfMetal);
        void CancelTransaction(Transaction transaction);
        void GiveChange(Transaction transaction);
        IEnumerable<string> CheckCoinReturn();
    }
}