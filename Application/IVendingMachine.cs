using System.Collections.Generic;

namespace Application
{
    public interface IVendingMachine
    {
        string CheckDisplay();
        void InsertCoin(string pieceOfMetal);
        void SelectProduct(string sku);
        IEnumerable<string> CheckCoinReturn();
        IEnumerable<string> CheckDispenser();
    }
}