﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ProductStatus
    {
        private HashSet<Product> _products;

        public ProductStatus(string sku, decimal price, int quantity)
        {
            _products = new HashSet<Product>();

            Sku = sku;
            Price = price;

            for (var i = 0; i < quantity; i++)
                _products.Add(Product.CreateProduct(sku));
        }

        public string Sku { get; private init; }
        public decimal Price { get; private init; }
        public bool IsAvailable => _products.Any();
        public IReadOnlyCollection<Product> Products => _products;

        public Product GetProduct()
        {
            var product = _products.First();
            _products.Remove(product);

            return product;
        }
    }

    public class Product
    {
        private Product(string sku, string name)
        {
            Sku = sku;
            Name = name;
        }

        public string Sku { get; private init; }
        public string Name { get; private init; }

        public static Product CreateProduct(string sku) => sku switch
        {
            "a" => new Product("a", "cola"),
            "b" => new Product("a", "chips"),
            "c" => new Product("a", "candy"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}