using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Transaction
    {
        private readonly List<Coin> _coins;
        private readonly List<Product> _products;
        

        public Transaction()
        {
            _coins = new List<Coin>();
            _products = new List<Product>();
        }

        public decimal InsertedCoinsTotal => _coins.Sum(x => x.Value);
        public decimal TotalCost => _products.Sum(x => x.Price);
        public decimal ChangeRequired => InsertedCoinsTotal - TotalCost;

        public bool IsSufficientPayment => InsertedCoinsTotal >= TotalCost;
        public bool IsCompleteAttempted { get; private set; }
        public bool IsComplete { get; private set; }
        
        public IReadOnlyCollection<Coin> Coins => _coins;
        public IReadOnlyCollection<Product> Products => _products;

        public void AddCoin(Coin coin) => _coins.Add(coin);
        public void AddProduct(Product product) => _products.Add(product);

        public string GetNotification()
        {
            if (_isPriceNotificationRequired)
            {
                _isPriceNotificationRequired = false;
                return PriceNotification;
            }

            if (_isCompleteNotificationRequired)
            {
                _isCompleteNotificationRequired = false;
                return CompleteNotification;
            }

            return IsCompleteAttempted && !IsComplete
                       ? "INSERT COINS"
                       : InsertedCoinsTotal > 0
                           ? $"{InsertedCoinsTotal:C}"
                           : "INSERT COINS";
        }

        public bool TryComplete()
        {
            IsCompleteAttempted = true;

            if (IsSufficientPayment)
            {
                _isCompleteNotificationRequired = true;
                IsComplete = true;
            }
            else
            {
                _isPriceNotificationRequired = true;
                IsComplete = false;
            }

            return IsComplete;
        }

        private bool _isPriceNotificationRequired;
        private string PriceNotification => $"PRICE {TotalCost:C}";

        private bool _isCompleteNotificationRequired;
        private string CompleteNotification => "THANK YOU";
    }
}