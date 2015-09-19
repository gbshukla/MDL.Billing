namespace MDL.Billing
{
    public interface IProduct
    {
        // ID of the item
        long Id { get; set; }

        // String based code of the item
        string Code { get; set; }

        // Name of the item
        string Name { get; set; }

        // Type of the item
        ProductType Type { get; set; }

        // Selling Price
        float Price { get; set; }
    }
    public class Product : IProduct
    {
        // Dependency to ensure product have necessary values.
        public Product(string code, string name, ProductType type, float price)
        {
            Code = code;
            Name = name;
            Type = type;
            Price = price;
        }
        // ID of the item
        public long Id { get; set; }

        // String based code of the item
        public string Code { get; set; }

        // Name of the item
        public string Name { get; set; }

        // Type of the item
        public ProductType Type { get; set; }

        // Selling Price
        public float Price { get; set; }
    }

    /// <summary>
    /// Product types
    /// </summary>
    public enum ProductType
    {
        Clothing,
        Grocery,
        Furnuture,
        Sports,
        Electronics,
        Toys
    }
}
