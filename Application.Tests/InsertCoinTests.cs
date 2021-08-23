using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests
{
    public class InsertCoinTests
    {
        private readonly IServiceProvider _serviceProvider;

        public InsertCoinTests()
        {
            _serviceProvider = TestDependencyResolver.Resolve();
        }

        private IVendingMachine GetTarget()
        {
            return _serviceProvider.GetService<IVendingMachine>();
        }

        [Theory]
        [InlineData("pieceOfMetal")]
        [InlineData("pieceOfMetal", "1cent")]
        [InlineData("1euro", "nickel")]
        public void WhenInvalidCoinInserted_GetsAddedToCoinReturn(params string[] piecesOfMetal)
        {
            var target = GetTarget();

            foreach (var pieceOfMetal in piecesOfMetal)
                target.InsertCoin(pieceOfMetal);

            var response = target.CheckCoinReturn();

            Assert.Equal(piecesOfMetal, response);
        }
    }
}