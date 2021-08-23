using System.Collections.Generic;

namespace Application
{
    public interface IStoreCoins
    {
        decimal GetTotal();
        void AddCoin(string pieceOfMetal);
        IEnumerable<string> CheckCoinReturn();
        void Empty();
        bool IsExpectingCoins { get; }
        void SetExpectingCoins(bool isExpectingCoins);
    }
}