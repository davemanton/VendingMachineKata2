using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.Tests.StockManager
{
    public class ProductDispenserTests
    {
        private readonly ICollection<CoinStatus> _storedCoins;
        private readonly ICollection<ProductStatus> _productStatus;

        private IServiceProvider _serviceProvider;

        public ProductDispenserTests()
        {
            _storedCoins = new HashSet<CoinStatus>()
            {
                new(CoinType.Penny, 5),
                new(CoinType.TwoPence, 5),
                new(CoinType.FivePence, 5),
                new(CoinType.TenPence, 5),
                new(CoinType.TwentyPence, 5),
                new(CoinType.FiftyPence, 5),
                new(CoinType.OnePound, 5),
                new(CoinType.TwoPounds, 5),
            };

            _productStatus = new HashSet<ProductStatus>()
            {
                new("a", 1.00m, 3),
                new("b", 0.50m, 3),
                new("c", 0.65m, 3),
            };
        }

        public IVendingMachine GetTarget()
        {
            _serviceProvider = TestDependencyResolver.Resolve(_productStatus, _storedCoins);

            return _serviceProvider.GetRequiredService<IVendingMachine>();
        }

        [Theory]
        [InlineData("cola", "a", "pound")]
        [InlineData("cola", "a", "2pounds")]
        [InlineData("chips", "b", "fifty")]
        [InlineData("chips", "b", "pound")]
        [InlineData("candy", "c", "pound")]
        [InlineData("candy", "c", "fifty", "twenty")]
        public void WhenProductIsPaidFor_AndSkuEntered_ProductIsDispensed(string product, string sku, params string[] coins)
        {
            var target = GetTarget();

            foreach(var coin in coins)
                target.InsertCoin(coin);

            target.SelectProduct(sku);

            var response = target.CheckDispenser();

            Assert.Contains(product, response);
        }

        [Theory]
        [InlineData("a", "pound")]
        [InlineData("a", "2pounds")]
        [InlineData("b", "fifty")]
        [InlineData("b", "pound")]
        [InlineData("c", "pound")]
        [InlineData("c", "fifty", "twenty")]
        public void WhenProductIsPaidFor_AndSkuEntered_DisplaySaysThankyou(string sku, params string[] coins)
        {
            var target = GetTarget();

            foreach (var coin in coins)
                target.InsertCoin(coin);

            target.SelectProduct(sku);

            var response = target.CheckDisplay();

            Assert.Equal("THANK YOU", response);
        }

        [Theory]
        [InlineData("a", "pound")]
        [InlineData("a", "2pounds")]
        [InlineData("b", "fifty")]
        [InlineData("b", "pound")]
        [InlineData("c", "pound")]
        [InlineData("c", "fifty", "twenty")]
        public void WhenProductIsPaidFor_AndSkuEntered_DisplaySaysInsertCoins_AfterThankyou(string sku, params string[] coins)
        {
            var target = GetTarget();

            foreach (var coin in coins)
                target.InsertCoin(coin);

            target.SelectProduct(sku);

            target.CheckDisplay();
            var response = target.CheckDisplay();

            Assert.Equal("INSERT COINS", response);
        }

        [Theory]
        [InlineData("a", "2pounds", "pound")]
        [InlineData("b", "2pounds", "pound", "fifty")]
        [InlineData("c", "2pounds", "pound", "twenty", "ten", "five")]
        public void WhenProductIsPaidFor_ChangeIsDispensed(string sku, string paymentCoin, params string[] expectedChange)
        {
            var target = GetTarget();

            target.InsertCoin(paymentCoin);

            target.SelectProduct(sku);

            var response = target.CheckCoinReturn();

            Assert.Equal(expectedChange, response);
        }

        [Theory]
        [InlineData("a", "2pounds", "twenty", "twenty", "twenty", "twenty", "twenty")]
        [InlineData("b", "2pounds", "twenty", "twenty", "twenty", "twenty", "twenty", "ten", "ten", "ten", "ten", "ten")]
        [InlineData("c", "2pounds", "twenty", "twenty", "twenty", "twenty", "twenty", "ten", "ten", "ten", "five")]
        public void WhenProductIsPaidFor_AndNoPoundsOrFiftiesAvailable_ChangeIsDispensed(string sku, string paymentCoin, params string[] expectedChange)
        {
            var poundStatus = _storedCoins.Single(x => x.CoinType == CoinType.OnePound);
            _storedCoins.Remove(poundStatus);
            _storedCoins.Add(new CoinStatus(CoinType.OnePound, 0));

            var fiftyStatus = _storedCoins.Single(x => x.CoinType == CoinType.FiftyPence);
            _storedCoins.Remove(fiftyStatus);
            _storedCoins.Add(new CoinStatus(CoinType.FiftyPence, 0));

            var target = GetTarget();

            target.InsertCoin(paymentCoin);

            target.SelectProduct(sku);

            var response = target.CheckCoinReturn();

            Assert.Equal(expectedChange, response);
        }

        [Theory]
        [InlineData(1.00, "a", "fifty", "twenty", "twenty", "five", "two", "two")]
        [InlineData(0.50, "b", "twenty", "twenty", "five", "two", "two")]
        [InlineData(0.65, "c", "fifty", "ten", "two", "two")]
        public void WhenInsufficientFundsInserted_DisplaySaysPrice(decimal price, string sku, params string[] coins)
        {
            var target = GetTarget();

            foreach (var coin in coins)
                target.InsertCoin(coin);

            target.SelectProduct(sku);

            var response = target.CheckDisplay();

            Assert.Equal($"PRICE {price:C}", response);
        }

        [Theory]
        [InlineData(1.00, "a", "fifty", "twenty", "twenty", "five", "two", "two")]
        [InlineData(0.50, "b", "twenty", "twenty", "five", "two", "two")]
        [InlineData(0.65, "c", "fifty", "ten", "two", "two")]
        public void WhenInsufficientFundsInserted_DisplaySaysInsertCoins_AfterPrice(decimal price, string sku, params string[] coins)
        {
            var target = GetTarget();

            foreach (var coin in coins)
                target.InsertCoin(coin);

            target.SelectProduct(sku);

            target.CheckDisplay();
            var response = target.CheckDisplay();

            Assert.Equal($"INSERT COINS", response);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("c")]
        public void WhenProductIsSelected_IfOutOfStock_DisplaysSoldOut(string sku)
        {
            var productStatus = _productStatus.Single(x => x.Sku == sku);
            _productStatus.Remove(productStatus);
            var noStockStatus = new ProductStatus(sku, productStatus.Price, 0);
            _productStatus.Add(noStockStatus);

            var target = GetTarget();

            target.InsertCoin("2pounds");

            target.SelectProduct(sku);

            var response = target.CheckDisplay();

            Assert.Equal("SOLD OUT", response);
        }
    }
}