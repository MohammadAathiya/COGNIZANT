using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSearch
{
    /// <summary>
    /// Service class that implements various search algorithms
    /// </summary>
    public class SearchService
    {
        /// <summary>
        /// Linear Search Algorithm - O(n) time complexity
        /// Searches through the array sequentially
        /// </summary>
        /// <param name="products">Array of products (can be unsorted)</param>
        /// <param name="searchId">Product ID to search for</param>
        /// <returns>Found product or null</returns>
        public Product? LinearSearch(Product[] products, int searchId)
        {
            if (products == null || products.Length == 0)
            {
                Console.WriteLine("Linear Search: Product array is empty or null");
                return null;
            }

            int comparisons = 0;

            for (int i = 0; i < products.Length; i++)
            {
                comparisons++;
                if (products[i].ProductId == searchId)
                {
                    Console.WriteLine($"Linear Search: Found at index {i} after {comparisons} comparisons");
                    return products[i];
                }
            }
            
            Console.WriteLine($"Linear Search: Product ID {searchId} not found after {comparisons} comparisons");
            return null;
        }

        /// <summary>
        /// Binary Search Algorithm - O(log n) time complexity
        /// Requires a sorted array
        /// </summary>
        /// <param name="sortedProducts">Array of products sorted by ProductId</param>
        /// <param name="searchId">Product ID to search for</param>
        /// <returns>Found product or null</returns>
        public Product? BinarySearch(Product[] sortedProducts, int searchId)
        {
            if (sortedProducts == null || sortedProducts.Length == 0)
            {
                Console.WriteLine("Binary Search: Product array is empty or null");
                return null;
            }

            int left = 0;
            int right = sortedProducts.Length - 1;
            int steps = 0;

            while (left <= right)
            {
                steps++;
                int mid = left + (right - left) / 2; // Prevents overflow

                if (sortedProducts[mid].ProductId == searchId)
                {
                    Console.WriteLine($"Binary Search: Found at index {mid} in {steps} steps");
                    return sortedProducts[mid];
                }
                else if (sortedProducts[mid].ProductId < searchId)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            Console.WriteLine($"Binary Search: Product ID {searchId} not found in {steps} steps");
            return null;
        }

        /// <summary>
        /// Searches for products by name (partial match)
        /// </summary>
        /// <param name="products">Array of products</param>
        /// <param name="searchName">Name or partial name to search for</param>
        /// <returns>List of matching products</returns>
        public List<Product> SearchByName(Product[] products, string searchName)
        {
            var results = new List<Product>();
            
            if (products == null || products.Length == 0)
            {
                Console.WriteLine("Search by Name: Product array is empty or null");
                return results;
            }

            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Search by Name: Search term is empty");
                return results;
            }

            int matches = 0;
            foreach (var product in products)
            {
                if (product.ProductName.ToLower().Contains(searchName.ToLower()))
                {
                    results.Add(product);
                    matches++;
                }
            }

            Console.WriteLine($"Search by Name: Found {matches} products matching '{searchName}'");
            return results;
        }

        /// <summary>
        /// Searches for products by category
        /// </summary>
        public List<Product> SearchByCategory(Product[] products, string category)
        {
            var results = new List<Product>();
            
            if (products == null || products.Length == 0 || string.IsNullOrWhiteSpace(category))
                return results;

            foreach (var product in products)
            {
                if (product.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(product);
                }
            }

            Console.WriteLine($"Search by Category: Found {results.Count} products in '{category}'");
            return results;
        }

        /// <summary>
        /// Sorts products by ProductId using Array.Sort
        /// </summary>
        public Product[] SortProductsById(Product[] products)
        {
            if (products == null || products.Length == 0)
                return Array.Empty<Product>();

            var sortedArray = new Product[products.Length];
            Array.Copy(products, sortedArray, products.Length);
            
            // Using the CompareTo method defined in Product class
            Array.Sort(sortedArray, (p1, p2) => p1.ProductId.CompareTo(p2.ProductId));
            
            Console.WriteLine($"Sort Products: Sorted {sortedArray.Length} products by ID");
            return sortedArray;
        }

        /// <summary>
        /// Performs performance comparison between Linear and Binary Search
        /// </summary>
        public void ComparePerformance(Product[] products, int searchId)
        {
            Console.WriteLine("\n--- PERFORMANCE COMPARISON ---");
            var stopwatch = new System.Diagnostics.Stopwatch();

            // Linear Search Performance
            stopwatch.Start();
            var linearResult = LinearSearch(products, searchId);
            stopwatch.Stop();
            var linearTime = stopwatch.ElapsedTicks;

            // Binary Search Performance (with sorting)
            stopwatch.Reset();
            stopwatch.Start();
            var sortedProducts = SortProductsById(products);
            var binaryResult = BinarySearch(sortedProducts, searchId);
            stopwatch.Stop();
            var binaryTime = stopwatch.ElapsedTicks;

            Console.WriteLine($"Linear Search Time: {linearTime} ticks");
            Console.WriteLine($"Binary Search Time: {binaryTime} ticks");
            
            if (binaryTime > 0)
            {
                Console.WriteLine($"Binary Search is {(double)linearTime / binaryTime:F2}x faster than Linear Search");
            }
            else
            {
                Console.WriteLine("Binary Search was extremely fast (less than 1 tick)");
            }

            Console.WriteLine($"\nLinear Search comparisons: O(n) = up to {products.Length} comparisons");
            Console.WriteLine($"Binary Search comparisons: O(log n) = up to {Math.Ceiling(Math.Log2(products.Length))} comparisons");
        }
    }
}