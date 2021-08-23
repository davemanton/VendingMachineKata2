using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IDispenseProducts
    {
        void DispenseProduct(string sku);
        IEnumerable<string> CheckProductHopper();
    }
}