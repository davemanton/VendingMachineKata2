using System.Collections.Generic;

namespace Application
{
    public interface IVendingMachine
    {
        string CheckDisplay();
        void InsertCoin(string pieceOfMetal);
        IEnumerable<string> CheckCoinReturn();
    }
}