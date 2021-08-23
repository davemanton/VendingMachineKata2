using System.Collections.Generic;

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
}
