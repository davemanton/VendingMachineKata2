using System.Collections.Generic;
using Domain;

namespace Application
{
    public interface IDispenseProducts
    {
        void DispenseProduct(Transaction transaction, string sku);
        IEnumerable<string> CheckProductHopper();
    }
}