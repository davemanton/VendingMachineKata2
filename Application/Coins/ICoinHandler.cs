using System.Collections.Generic;

namespace Application
{
    public interface ICoinHandler
    {
        void InsertCoin(string pieceOfMetal);
        IEnumerable<string> CheckCoinReturn();
    }
}