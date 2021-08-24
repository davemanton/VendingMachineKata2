using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface ICalculateChange
    {
        bool TryCalculate(ICollection<CoinStatus> availableCoins, decimal changeRequired, out ICollection<Coin> change);
    }
}