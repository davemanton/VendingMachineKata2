using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application
{
    public class ProductDispenser : IDispenseProducts
    {
        private readonly HashSet<ProductStatus> _products;
        private readonly ICollection<string> _dispenser;

        private readonly IDisplayMessages _messageDisplay;
        private readonly IStoreCoins _coinStore;

        private ProductDispenser(IStoreCoins coinStore)
        {
            _coinStore = coinStore;
            _dispenser = new List<string>();
        }

        public ProductDispenser(HashSet<ProductStatus> products, 
                                IDisplayMessages messageDisplay,
                                IStoreCoins coinStore)
            : this(coinStore)
        {
            _products = products;
            _messageDisplay = messageDisplay;
        }

        public void DispenseProduct(string sku)
        {
            var product = _products.Single(x => x.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

            if (_coinStore.GetTotal() < product.Cost)
            {
                _coinStore.SetExpectingCoins(true);
                _messageDisplay.OverrideDisplay($"PRICE {product.Cost:C}");
                
                return;
            }

            _dispenser.Add(product.Product);
            _coinStore.Empty();

            _messageDisplay.OverrideDisplay("THANK YOU");
        }

        public IEnumerable<string> CheckProductHopper() { return _dispenser; }
    }
}