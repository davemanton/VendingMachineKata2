using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface ICalculateChange
    {
        IEnumerable<Coin> Calculate(decimal value);
    }
}