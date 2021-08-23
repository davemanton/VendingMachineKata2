namespace Application
{
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
}