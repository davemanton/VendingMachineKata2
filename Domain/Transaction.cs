using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Transaction
    {
        private readonly List<Coin> _coins;
        private readonly List<Product> _products;
        private string? _notification;

        public Transaction()
        {
            _coins = new List<Coin>();
            _products = new List<Product>();
            TotalCost = 0m;
        }

        public decimal InsertedCoinsTotal => _coins.Sum(x => x.Value);
        public decimal TotalCost { get; private set; }
        public decimal ChangeRequired => InsertedCoinsTotal - TotalCost;

        public bool IsSufficientPayment => InsertedCoinsTotal >= TotalCost;
        public bool IsCompleteAttempted { get; private set; }
        public bool IsComplete { get; private set; }
        
        public IReadOnlyCollection<Coin> Coins => _coins;
        public IReadOnlyCollection<Product> Products => _products;

        public void AddCoin(Coin coin) => _coins.Add(coin);

        public bool TryAddProduct(ProductStatus productStatus)
        {
            if (productStatus.IsAvailable)
            {
                _products.Add(productStatus.GetProduct());
                TotalCost += productStatus.Price;

                return true;
            }

            _notification = "SOLD OUT";
            return false;
        }

        public string GetNotification()
        {
            var notification = _notification;
            _notification = null;

            if (notification == null)
            {
                if ((IsCompleteAttempted && !IsComplete) || InsertedCoinsTotal == 0)
                    notification = "INSERT COINS";
                else
                {
                    notification = $"{InsertedCoinsTotal:C}";
                }
            }

            return notification;
        }

        public bool TryComplete()
        {
            IsCompleteAttempted = true;

            if (IsSufficientPayment)
            {
                _notification = "THANK YOU";
                IsComplete = true;
            }
            else
            {
                _notification = $"PRICE {TotalCost:C}";
                IsComplete = false;
            }

            return IsComplete;
        }
    }
}