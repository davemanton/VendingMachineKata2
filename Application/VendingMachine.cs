using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class VendingMachine : IVendingMachine
    {
        private readonly IStoreCoins _coinStore;
        private readonly IDisplayMessages _machineDisplay;
        private readonly IDispenseProducts _productDispenser;

        public VendingMachine(IStoreCoins coinStore, 
                              IDisplayMessages machineDisplay, 
                              IDispenseProducts productDispenser)
        {
            _coinStore = coinStore;
            _machineDisplay = machineDisplay;
            _productDispenser = productDispenser;
        }

        public string CheckDisplay()
        {
            return _machineDisplay.CheckDisplay();
        }

        public void InsertCoin(string pieceOfMetal)
        {
            _coinStore.AddCoin(pieceOfMetal);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            return _coinStore.CheckCoinReturn();
        }

        public void SelectProduct(string sku)
        {
            _productDispenser.DispenseProduct(sku);
        }

        public IEnumerable<string> CheckDispenser()
        {
            return _productDispenser.CheckProductHopper();
        }
    }

    public interface IDisplayMessages
    {
        string CheckDisplay();
        void OverrideDisplay(string message);
    }

    public class MachineDisplay : IDisplayMessages
    {
        private readonly IStoreCoins _coinStore;

        private string? _overrideMessage;

        public MachineDisplay(IStoreCoins coinStore)
        {
            _coinStore = coinStore;
        }

        public string CheckDisplay()
        {
            var message = _overrideMessage ?? GetCoinStatus();
            
            _overrideMessage = null;

            return message;
        }

        public void OverrideDisplay(string message) => _overrideMessage = message;

        private string GetCoinStatus()
            => _coinStore.IsExpectingCoins
                   ? "INSERT COINS"
                   : $"£{_coinStore.GetTotal()}";
    }

    public interface IStoreCoins
    {
        decimal GetTotal();
        void AddCoin(string pieceOfMetal);
        IEnumerable<string> CheckCoinReturn();
        void Empty();
        bool IsExpectingCoins { get; }
        void SetExpectingCoins(bool isExpectingCoins);
    }

    public class CoinStore : IStoreCoins
    {
        private readonly ICollection<Coin> _coins;
        private readonly ICollection<string> _coinReturn;

        private readonly IDetectCoins _coinDetector;

        private CoinStore()
        {
            _coins = new List<Coin>();
            _coinReturn = new List<string>();
        }

        public CoinStore(IDetectCoins coinDetector) 
            : this()
        {
            _coinDetector = coinDetector;
        }

        public decimal GetTotal()
        {
            return _coins.Sum(x => x.Value);
        }

        public void AddCoin(string pieceOfMetal)
        {
            var coin = _coinDetector.Detect(pieceOfMetal);

            if (coin != null)
                _coins.Add(coin);
            else
                _coinReturn.Add(pieceOfMetal);
        }

        public IEnumerable<string> CheckCoinReturn()
        {
            var returnedCoins = _coinReturn.ToList();

            _coinReturn.Clear();

            return returnedCoins;
        }

        public void Empty() => _coins.Clear();
        
        private bool _isExpectingCoins;
        public bool IsExpectingCoins => _coins.Sum(x => x.Value) == 0 || _isExpectingCoins;
        
        public void SetExpectingCoins(bool isExpectingCoins) { _isExpectingCoins = true; }
    }
}
