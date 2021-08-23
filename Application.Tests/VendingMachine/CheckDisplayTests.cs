using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Application.Tests
{
    public class CheckDisplayTests
    {
        private readonly IServiceProvider _serviceProvider;

        public CheckDisplayTests()
        {
            _serviceProvider = TestDependencyResolver.Resolve();
        }

        private IVendingMachine GetTarget()
        {
            return _serviceProvider.GetService<IVendingMachine>();
        }

        [Fact]
        public void WhenNoCoinsInserted_DisplaysInsertCoin()
        {
            var target = GetTarget();

            var response = target.CheckDisplay();

            Assert.Equal("INSERT COINS", response);
        }

        [Theory]
        [InlineData(0.01, "penny")]
        [InlineData(0.06, "penny", "five")]
        [InlineData(0.16, "penny", "five", "ten")]
        [InlineData(0.36, "penny", "five", "ten", "twenty")]
        [InlineData(0.86, "penny", "five", "ten", "twenty", "fifty")]
        [InlineData(1.86, "penny", "five", "ten", "twenty", "fifty", "pound")]
        [InlineData(3.86, "penny", "five", "ten", "twenty", "fifty", "pound", "2pounds")]
        public void WhenCoinsInserted_ShowsCurrentTotal(decimal totalValue, params string[] piecesOfMetal)
        {
            var target = GetTarget();

            foreach(var pieceOfMetal in piecesOfMetal)
                target.InsertCoin(pieceOfMetal);

            var response = target.CheckDisplay();

            Assert.Equal($"£{totalValue}", response);
        }

        [Theory]
        [InlineData(0.01, "penny", "badCoin")]
        [InlineData(0.06, "penny", "badCoin", "five")]
        [InlineData(0.16, "penny", "five", "ten", "badCoin")]
        [InlineData(0.36, "penny", "five", "badCoin", "ten", "twenty")]
        [InlineData(0.86, "penny", "five", "ten", "badCoin", "twenty", "fifty")]
        [InlineData(1.86, "penny", "five", "badCoin", "ten", "twenty", "fifty", "pound", "badCoin")]
        [InlineData(3.86, "penny", "five", "badCoin", "ten", "twenty", "badCoin", "fifty", "pound", "2pounds", "badCoin")]
        public void WhenInvalidCoinsInserted_ShowsCorrectTotal(decimal totalValue, params string[] piecesOfMetal)
        {
            var target = GetTarget();

            foreach (var pieceOfMetal in piecesOfMetal)
                target.InsertCoin(pieceOfMetal);

            var response = target.CheckDisplay();

            Assert.Equal($"£{totalValue}", response);
        }
    }
}
