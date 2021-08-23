namespace Domain
{
    public class ProductStatus
    {
        public ProductStatus(string sku, string product, decimal cost)
        {
            Sku = sku;
            Product = product;
            Cost = cost;
        }

        public string Sku { get; private init; }
        public string Product { get; private init; }
        public decimal Cost { get; private init; }
    }
}