using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FinancialForecasting
{
    /// <summary>
    /// Analyzes and compares performance of different recursive implementations
    /// </summary>
    public class PerformanceAnalyzer
    {
        private readonly FinancialCalculator _calculator;

        public PerformanceAnalyzer()
        {
            _calculator = new FinancialCalculator();
        }

        /// <summary>
        /// Benchmarks different recursive implementations
        /// </summary>
        public void BenchmarkRecursiveMethods(decimal presentValue, decimal growthRate, int periods)
        {
            Console.WriteLine("\n=== PERFORMANCE BENCHMARK ===");
            Console.WriteLine($"Present Value: ${presentValue:F2}");
            Console.WriteLine($"Growth Rate: {growthRate:P2}");
            Console.WriteLine($"Periods: {periods}");
            Console.WriteLine(new string('-', 60));

            var stopwatch = new Stopwatch();

            // 1. Simple Recursive
            stopwatch.Start();
            var simpleResult = _calculator.CalculateFutureValueRecursive(presentValue, growthRate, periods);
            stopwatch.Stop();
            var simpleTime = stopwatch.ElapsedTicks;

            // 2. Tail Recursive
            stopwatch.Reset();
            stopwatch.Start();
            var tailResult = _calculator.CalculateFutureValueTailRecursive(presentValue, growthRate, periods);
            stopwatch.Stop();
            var tailTime = stopwatch.ElapsedTicks;

            // 3. Memoized Recursive
            stopwatch.Reset();
            stopwatch.Start();
            var memoResult = _calculator.CalculateFutureValueWithMemoization(presentValue, growthRate, periods);
            stopwatch.Stop();
            var memoTime = stopwatch.ElapsedTicks;

            // 4. Iterative (for comparison)
            stopwatch.Reset();
            stopwatch.Start();
            var iterativeResult = CalculateFutureValueIterative(presentValue, growthRate, periods);
            stopwatch.Stop();
            var iterativeTime = stopwatch.ElapsedTicks;

            // Display results
            Console.WriteLine("\n📊 Results Comparison:");
            Console.WriteLine($"  Simple Recursive:   ${simpleResult:F2} in {simpleTime} ticks");
            Console.WriteLine($"  Tail Recursive:     ${tailResult:F2} in {tailTime} ticks");
            Console.WriteLine($"  Memoized Recursive: ${memoResult:F2} in {memoTime} ticks");
            Console.WriteLine($"  Iterative:          ${iterativeResult:F2} in {iterativeTime} ticks");

            // Performance analysis
            Console.WriteLine("\n🚀 Performance Analysis:");
            Console.WriteLine($"  Simple vs Tail:     {(double)simpleTime / tailTime:F2}x faster");
            Console.WriteLine($"  Simple vs Memoized: {(double)simpleTime / memoTime:F2}x faster");
            Console.WriteLine($"  Iterative vs Simple:{(double)iterativeTime / simpleTime:F2}x faster");
        }

        /// <summary>
        /// Iterative version for comparison
        /// </summary>
        private decimal CalculateFutureValueIterative(decimal presentValue, decimal growthRate, int periods)
        {
            decimal value = presentValue;
            for (int i = 0; i < periods; i++)
            {
                value *= (1 + growthRate);
            }
            return value;
        }

        /// <summary>
        /// Analyzes recursive call stack usage
        /// </summary>
        public void AnalyzeCallStackDepth(int maxPeriods)
        {
            Console.WriteLine("\n=== CALL STACK ANALYSIS ===");
            Console.WriteLine($"Maximum periods: {maxPeriods}");
            Console.WriteLine("Note: Stack overflow risk increases with period count");
            Console.WriteLine($"Estimated stack size per call: ~32-64 bytes");
            Console.WriteLine($"Risk threshold: ~100,000 periods (approx)");
            
            // Determine if optimization is needed
            if (maxPeriods > 10000)
            {
                Console.WriteLine("⚠️  WARNING: Consider iterative or tail-recursive optimization!");
                Console.WriteLine($"   Simple recursion with {maxPeriods} periods may cause stack overflow");
            }
            else
            {
                Console.WriteLine("✅ Stack usage is within safe limits");
            }
        }

        /// <summary>
        /// Compares prediction accuracy with actual data (for testing)
        /// </summary>
        public void ValidatePredictions(decimal[] historicalData, decimal[] actualFutureData)
        {
            Console.WriteLine("\n=== PREDICTION VALIDATION ===");
            
            if (historicalData == null || actualFutureData == null || 
                historicalData.Length == 0 || actualFutureData.Length == 0)
            {
                Console.WriteLine("Insufficient data for validation");
                return;
            }

            var predictions = _calculator.PredictFutureValues(historicalData, actualFutureData.Length);
            
            Console.WriteLine("Prediction Accuracy Comparison:");
            Console.WriteLine("Period | Predicted | Actual | Error");
            Console.WriteLine(new string('-', 45));

            for (int i = 0; i < predictions.Count && i < actualFutureData.Length; i++)
            {
                decimal error = Math.Abs(predictions[i].PredictedValue - actualFutureData[i]) / actualFutureData[i] * 100;
                Console.WriteLine($"  {i+1,2}   | ${predictions[i].PredictedValue,8:F2} | ${actualFutureData[i],6:F2} | {error,5:F2}%");
            }
        }
    }
}