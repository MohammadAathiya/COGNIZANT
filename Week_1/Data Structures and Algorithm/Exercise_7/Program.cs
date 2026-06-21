using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialForecasting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== FINANCIAL FORECASTING TOOL ===");
            Console.WriteLine("Recursive Prediction Algorithms\n");

            var calculator = new FinancialCalculator();
            var analyzer = new PerformanceAnalyzer();

            // ============ DEMONSTRATION 1: BASIC RECURSIVE ============
            Console.WriteLine(" DEMONSTRATION 1: Basic Future Value Calculation");
            Console.WriteLine(new string('-', 50));

            decimal presentValue = 10000m;
            decimal growthRate = 0.05m; // 5% annual growth
            int periods = 10;

            Console.WriteLine($"Initial Investment: ${presentValue:F2}");
            Console.WriteLine($"Annual Growth Rate: {growthRate:P2}");
            Console.WriteLine($"Investment Period: {periods} years\n");

            var result = calculator.CalculateFutureValueRecursive(presentValue, growthRate, periods);
            Console.WriteLine($"Future Value (Simple Recursion): ${result:F2}");

            var memoResult = calculator.CalculateFutureValueWithMemoization(presentValue, growthRate, periods);
            Console.WriteLine($"Future Value (Memoized): ${memoResult:F2}");

            var tailResult = calculator.CalculateFutureValueTailRecursive(presentValue, growthRate, periods);
            Console.WriteLine($"Future Value (Tail Recursive): ${tailResult:F2}");

            // ============ DEMONSTRATION 2: HISTORICAL PREDICTION ============
            Console.WriteLine("\n DEMONSTRATION 2: Prediction Based on Historical Data");
            Console.WriteLine(new string('-', 50));

            decimal[] historicalData = { 10000m, 10500m, 11025m, 11576m, 12155m, 12763m, 13401m, 14071m };
            
            Console.WriteLine("Historical Data:");
            for (int i = 0; i < historicalData.Length; i++)
            {
                Console.WriteLine($"  Year {i+1}: ${historicalData[i]:F2}");
            }

            int predictionYears = 5;
            var predictions = calculator.PredictFutureValues(historicalData, predictionYears);

            Console.WriteLine($"\nPredicted Values (Next {predictionYears} years):");
            foreach (var pred in predictions)
            {
                Console.WriteLine($"  {pred}");
            }

            // ============ DEMONSTRATION 3: ANNUITY ============
            Console.WriteLine("\n DEMONSTRATION 3: Annuity Future Value");
            Console.WriteLine(new string('-', 50));

            decimal monthlyPayment = 500m;
            decimal annualRate = 0.08m;
            int years = 30;

            decimal annuityFV = calculator.CalculateAnnuityFutureValue(
                monthlyPayment, 
                annualRate / 12, 
                years * 12);

            Console.WriteLine($"Monthly Payment: ${monthlyPayment:F2}");
            Console.WriteLine($"Annual Rate: {annualRate:P2}");
            Console.WriteLine($"Term: {years} years");
            Console.WriteLine($"Future Value of Annuity: ${annuityFV:F2}");

            // ============ DEMONSTRATION 4: SENSITIVITY ANALYSIS ============
            Console.WriteLine("\n DEMONSTRATION 4: Sensitivity Analysis");
            Console.WriteLine(new string('-', 50));

            decimal[] growthRates = { 0.03m, 0.05m, 0.07m, 0.10m };
            var sensitivityResults = calculator.SensitivityAnalysis(10000m, 5, growthRates);

            Console.WriteLine("Future Value at Different Growth Rates:");
            Console.WriteLine("Period |  3%      |  5%      |  7%      |  10%");
            Console.WriteLine(new string('-', 50));

            for (int i = 1; i <= 5; i++)
            {
                Console.Write($"  {i,2}   ");
                foreach (var rate in growthRates)
                {
                    var value = sensitivityResults[rate][i-1].PredictedValue;
                    Console.Write($"| ${value,6:F2} ");
                }
                Console.WriteLine();
            }

            // ============ DEMONSTRATION 5: PERFORMANCE ANALYSIS ============
            Console.WriteLine("\n DEMONSTRATION 5: Performance Analysis");
            Console.WriteLine(new string('-', 50));

            analyzer.BenchmarkRecursiveMethods(10000m, 0.05m, 20);
            analyzer.AnalyzeCallStackDepth(50);

            // ============ DEMONSTRATION 6: VALIDATION ============
            Console.WriteLine("\n DEMONSTRATION 6: Prediction Validation");
            Console.WriteLine(new string('-', 50));

            decimal[] validationHistorical = { 10000m, 10500m, 11025m, 11576m, 12155m };
            decimal[] actualFuture = { 12763m, 13401m, 14071m, 14775m, 15513m };
            
            analyzer.ValidatePredictions(validationHistorical, actualFuture);

            // ============ ANALYSIS SUMMARY ============
            Console.WriteLine("\nANALYSIS SUMMARY");
            Console.WriteLine(new string('=', 60));
            DisplayAnalysis(periods);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DisplayAnalysis(int periods)
        {
            Console.WriteLine("\nTime Complexity Analysis:");
            Console.WriteLine($"  Simple Recursion: O(n) where n = {periods} periods");
            Console.WriteLine("    - Each call makes one recursive call");
            Console.WriteLine("    - Space complexity: O(n) due to call stack");
            
            Console.WriteLine("\n   Memoized Recursion: O(n)");
            Console.WriteLine("    - Reduces redundant calculations");
            Console.WriteLine("    - Space complexity: O(n) for cache");
            
            Console.WriteLine("\n   Tail Recursion: O(n)");
            Console.WriteLine("    - Can be optimized by compiler");
            Console.WriteLine("    - Space complexity: O(1) if optimized");

            Console.WriteLine("\nOptimization Strategies:");
            Console.WriteLine("  1. Memoization: Cache intermediate results");
            Console.WriteLine("  2. Tail Recursion: Reduce stack usage");
            Console.WriteLine("  3. Iterative Conversion: Eliminate recursion overhead");
            Console.WriteLine("  4. Divide & Conquer: Use parallel processing for large datasets");

            Console.WriteLine("\nBest Practice Recommendations:");
            Console.WriteLine("  • For < 100 periods: Simple recursion is fine");
            Console.WriteLine("  • For 100-1000 periods: Use memoization");
            Console.WriteLine("  • For > 1000 periods: Use iterative or tail recursion");
            Console.WriteLine("  • For financial applications: Use decimal for precision");
            Console.WriteLine("  • Always validate with historical data when possible");
        }
    }
}