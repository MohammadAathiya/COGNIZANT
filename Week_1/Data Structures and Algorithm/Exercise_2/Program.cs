using System;
using System.Collections.Generic;

namespace ECommerceSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-COMMERCE PLATFORM SEARCH DEMONSTRATION ===\n");
            
            // Initialize the search service
            var searchService = new SearchService();
            
            // Create sample product catalog
            var products = CreateSampleProducts();
            
            // Display the product catalog
            DisplayProducts(products, "Product Catalog");
            
            // Run search demonstrations
            RunLinearSearchDemo(products, searchService);
            RunBinarySearchDemo(products, searchService);
            RunNameSearchDemo(products, searchService);
            RunCategorySearchDemo(products, searchService);
            
            // Performance comparison
            searchService.ComparePerformance(products, 1005);
            
            // Show analysis
            DisplayAnalysis(products.Length);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Creates sample products for demonstration
        /// </summary>
        static Product[] CreateSampleProducts()
        {
            return new Product[]
            {
                new Product(1001, "iPhone 15 Pro", "Electronics", 999.99m),
                new Product(1002, "Samsung Galaxy S24", "Electronics", 899.99m),
                new Product(1003, "MacBook Pro 14\"", "Electronics", 1299.99m),
                new Product(1004, "Nike Air Max", "Footwear", 149.99m),
                new Product(1005, "Adidas Ultraboost", "Footwear", 179.99m),
                new Product(1006, "Sony WH-1000XM5", "Electronics", 299.99m),
                new Product(1007, "Coffee Maker", "Home & Kitchen", 79.99m),
                new Product(1008, "Desk Lamp", "Home & Kitchen", 39.99m),
                new Product(1009, "Backpack", "Accessories", 65.99m),
                new Product(1010, "Logitech MX Master", "Electronics", 49.99m),
                new Product(1011, "Gaming Keyboard", "Electronics", 89.99m),
                new Product(1012, "Dell Monitor 27\"", "Electronics", 299.99m),
                new Product(1013, "USB-C Cable", "Accessories", 19.99m),
                new Product(1014, "Phone Case", "Accessories", 24.99m),
                new Product(1015, "Power Bank 20000mAh", "Electronics", 59.99m),
                new Product(1016, "Running Shoes", "Footwear", 129.99m),
                new Product(1017, "Blender", "Home & Kitchen", 89.99m),
                new Product(1018, "Toaster", "Home & Kitchen", 49.99m),
                new Product(1019, "Wireless Charger", "Electronics", 39.99m),
                new Product(1020, "Laptop Stand", "Accessories", 34.99m)
            };
        }

        /// <summary>
        /// Displays products in a formatted way
        /// </summary>
        static void DisplayProducts(Product[] products, string title)
        {
            Console.WriteLine($"=== {title} ===");
            Console.WriteLine($"Total Products: {products.Length}");
            Console.WriteLine(new string('-', 80));
            
            foreach (var product in products)
            {
                Console.WriteLine($"  {product}");
            }
            Console.WriteLine(new string('-', 80));
            Console.WriteLine();
        }

        /// <summary>
        /// Runs Linear Search demonstrations
        /// </summary>
        static void RunLinearSearchDemo(Product[] products, SearchService searchService)
        {
            Console.WriteLine("=== LINEAR SEARCH DEMONSTRATION ===");
            Console.WriteLine("Time Complexity: O(n)");
            Console.WriteLine("Works on: Unsorted or Sorted Arrays");
            Console.WriteLine(new string('-', 50));

            // Test cases
            var testCases = new (int id, string description)[]
            {
                (1005, "Product exists (Adidas Ultraboost)"),
                (2000, "Product doesn't exist"),
                (1001, "First product (iPhone 15 Pro)"),
                (1020, "Last product (Laptop Stand)")
            };

            foreach (var testCase in testCases)
            {
                Console.WriteLine($"\nSearching for Product ID: {testCase.id} - {testCase.description}");
                var result = searchService.LinearSearch(products, testCase.id);
                if (result != null)
                {
                    Console.WriteLine($"  Found: {result}");
                }
                else
                {
                    Console.WriteLine($"   Product not found");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Runs Binary Search demonstrations
        /// </summary>
        static void RunBinarySearchDemo(Product[] products, SearchService searchService)
        {
            Console.WriteLine("=== BINARY SEARCH DEMONSTRATION ===");
            Console.WriteLine("Time Complexity: O(log n)");
            Console.WriteLine("Requires: Sorted Array by ID");
            Console.WriteLine(new string('-', 50));

            // Sort products first
            var sortedProducts = searchService.SortProductsById(products);
            
            Console.WriteLine("\nSorted Products (by ID):");
            Console.WriteLine($"First: {sortedProducts[0].ProductId} - {sortedProducts[0].ProductName}");
            Console.WriteLine($"Middle: {sortedProducts[sortedProducts.Length / 2].ProductId} - {sortedProducts[sortedProducts.Length / 2].ProductName}");
            Console.WriteLine($"Last: {sortedProducts[sortedProducts.Length - 1].ProductId} - {sortedProducts[sortedProducts.Length - 1].ProductName}");

            // Test cases
            var testCases = new (int id, string description)[]
            {
                (1005, "Product exists (Adidas Ultraboost)"),
                (2000, "Product doesn't exist"),
                (1001, "First product"),
                (1020, "Last product")
            };

            foreach (var testCase in testCases)
            {
                Console.WriteLine($"\nSearching for Product ID: {testCase.id} - {testCase.description}");
                var result = searchService.BinarySearch(sortedProducts, testCase.id);
                if (result != null)
                {
                    Console.WriteLine($"  Found: {result}");
                }
                else
                {
                    Console.WriteLine($"   Product not found");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Runs name-based search demonstration
        /// </summary>
        static void RunNameSearchDemo(Product[] products, SearchService searchService)
        {
            Console.WriteLine("=== NAME SEARCH DEMONSTRATION ===");
            Console.WriteLine("Using Linear Search with string matching");
            Console.WriteLine(new string('-', 50));

            var searchTerms = new[] { "Phone", "Shoes", "Cable", "Wireless" };

            foreach (var term in searchTerms)
            {
                Console.WriteLine($"\nSearching for: '{term}'");
                var results = searchService.SearchByName(products, term);
                
                if (results.Count > 0)
                {
                    Console.WriteLine($"  Found {results.Count} products:");
                    foreach (var product in results)
                    {
                        Console.WriteLine($"    • {product}");
                    }
                }
                else
                {
                    Console.WriteLine($"  No products found matching '{term}'");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Runs category-based search demonstration
        /// </summary>
        static void RunCategorySearchDemo(Product[] products, SearchService searchService)
        {
            Console.WriteLine("=== CATEGORY SEARCH DEMONSTRATION ===");
            Console.WriteLine(new string('-', 50));

            var categories = new[] { "Electronics", "Footwear", "Accessories", "Home & Kitchen" };

            foreach (var category in categories)
            {
                Console.WriteLine($"\nSearching category: '{category}'");
                var results = searchService.SearchByCategory(products, category);
                
                if (results.Count > 0)
                {
                    Console.WriteLine($"  Found {results.Count} products in {category}:");
                    foreach (var product in results)
                    {
                        Console.WriteLine($"    • {product}");
                    }
                }
                else
                {
                    Console.WriteLine($"  No products found in category '{category}'");
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays analysis and recommendations
        /// </summary>
        static void DisplayAnalysis(int productCount)
        {
            Console.WriteLine("=== ANALYSIS & RECOMMENDATIONS ===");
            Console.WriteLine(new string('-', 50));
            
            Console.WriteLine($"\n For {productCount} products:");
            Console.WriteLine($"  • Linear Search (Worst Case): O(n) = O({productCount}) comparisons");
            Console.WriteLine($"  • Binary Search (Worst Case): O(log n) = O({Math.Ceiling(Math.Log2(productCount))}) comparisons");
            Console.WriteLine($"  • Binary Search is approximately {productCount / Math.Ceiling(Math.Log2(productCount))}x faster in worst case");

            Console.WriteLine("\n Time Complexity Comparison:");
            Console.WriteLine("  ┌─────────────────┬──────────────┬──────────────┐");
            Console.WriteLine("  │   Operation     │ Linear Search│Binary Search │");
            Console.WriteLine("  ├─────────────────┼──────────────┼──────────────┤");
            Console.WriteLine($"  │ Best Case       │ O(1)         │ O(1)         │");
            Console.WriteLine($"  │ Average Case    │ O(n)         │ O(log n)     │");
            Console.WriteLine($"  │ Worst Case      │ O(n)         │ O(log n)     │");
            Console.WriteLine($"  │ Space           │ O(1)         │ O(1)         │");
            Console.WriteLine("  └─────────────────┴──────────────┴──────────────┘");

            Console.WriteLine("\nRecommendations:");
            Console.WriteLine("  1. Primary Search (by ID): Use Binary Search");
            Console.WriteLine("     → Fast O(log n) performance for large catalogs");
            Console.WriteLine("     → Maintain products sorted by ID");
            Console.WriteLine("     → Ideal for product detail lookups");
            
            Console.WriteLine("  2. Search by Name/Category: Use Linear Search");
            Console.WriteLine("     → Simple to implement with partial matches");
            Console.WriteLine("     → Good for small to medium datasets (< 1000 items)");
            Console.WriteLine("     → Consider full-text search for larger datasets");
            
            Console.WriteLine("  3. Optimized Solution:");
            Console.WriteLine("     → Use Dictionary<int, Product> for O(1) ID lookup");
            Console.WriteLine("     → Use HashSet for unique product IDs");
            Console.WriteLine("     → Implement inverted index for name searches");

            Console.WriteLine("\n Best Practice:");
            Console.WriteLine("  For e-commerce platforms with > 10,000 products:");
            Console.WriteLine("  Use Binary Search for exact ID matches");
            Console.WriteLine("  Use Database indexes for faster searches");
            Console.WriteLine("  Consider Elasticsearch for full-text search");
            Console.WriteLine("  Cache frequently searched items in memory");
        }
    }
}