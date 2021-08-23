using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface ICoinHandler
    {
        void InsertCoin(string pieceOfMetal);
        void GiveChange(Transaction transaction);
        IEnumerable<string> CheckCoinReturn();
    }
}