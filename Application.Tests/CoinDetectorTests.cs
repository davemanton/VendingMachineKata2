using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests
{
    public class CoinDetectorTests
    {
        private readonly IServiceProvider _serviceProvider;

        public CoinDetectorTests()
        {
            _serviceProvider = TestDependencyResolver.Resolve();
        }

        public IDetectCoins GetTarget()
        {
            return _serviceProvider.GetRequiredService<IDetectCoins>();
        }

        [Theory]
        [InlineData("penny", 0.01)]
        [InlineData("five", 0.05)]
        [InlineData("ten", 0.10)]
        [InlineData("twenty", 0.20)]
        [InlineData("fifty", 0.50)]
        [InlineData("pound", 1.00)]
        [InlineData("2pounds", 2.00)]
        public void DetectCoin_ReturnsCoin_WhenValidPieceOfMetalInserted(string pieceOfMetal, decimal expectedValue)
        {
            var target = GetTarget();

            var coin = target.Detect(pieceOfMetal);

            Assert.Equal(expectedValue, coin.Value);
        }

        [Theory]
        [InlineData("random")]
        [InlineData("penny2")]
        [InlineData("1pound")]
        [InlineData("2pound")]
        public void DetectCoin_ReturnsNull_WhenInvalidPieceOfMetalInserted(string pieceOfMetal)
        {
            var target = GetTarget();

            var coin = target.Detect(pieceOfMetal);

            Assert.Null(coin);
        }
    }
}