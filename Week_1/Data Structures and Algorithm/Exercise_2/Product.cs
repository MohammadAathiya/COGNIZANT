using System;

namespace ECommerceSearch
{
    /// <summary>
    /// Represents a product in the e-commerce platform
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Category of the product
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Constructor to create a new product
        /// </summary>
        public Product(int productId, string productName, string category, decimal price)
        {
            ProductId = productId;
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Price = price;
        }

        /// <summary>
        /// Returns a string representation of the product
        /// </summary>
        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Category: {Category}, Price: ${Price:F2}";
        }

        /// <summary>
        /// Compares two products by their ID (for sorting)
        /// </summary>
        public int CompareTo(Product? other)
        {
            if (other == null) return 1;
            return ProductId.CompareTo(other.ProductId);
        }

        /// <summary>
        /// Checks if two products are equal by their ID
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Product product && ProductId == product.ProductId;
        }

        /// <summary>
        /// Returns hash code based on ProductId
        /// </summary>
        public override int GetHashCode()
        {
            return ProductId.GetHashCode();
        }
    }
}