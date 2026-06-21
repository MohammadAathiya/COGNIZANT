using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialForecasting
{
    /// <summary>
    /// Core financial forecasting calculator using recursive algorithms
    /// </summary>
    public class FinancialCalculator
    {
        // ============ BASIC RECURSIVE FUTURE VALUE ============
        
        /// <summary>
        /// Calculates future value using simple recursive approach
        /// Formula: FV = PV * (1 + rate)^periods
        /// </summary>
        public decimal CalculateFutureValueRecursive(decimal presentValue, decimal growthRate, int periods)
        {
            // Base case: No more periods to compound
            if (periods == 0)
                return presentValue;
            
            // Recursive case: Compound one period and continue
            decimal newValue = presentValue * (1 + growthRate);
            return CalculateFutureValueRecursive(newValue, growthRate, periods - 1);
        }

        /// <summary>
        /// Optimized recursive calculation using memoization
        /// Avoids redundant calculations for same periods
        /// </summary>
        public decimal CalculateFutureValueWithMemoization(decimal presentValue, decimal growthRate, int periods)
        {
            // Use dictionary to cache results
            var memo = new Dictionary<int, decimal>();
            return CalculateFVWithMemo(presentValue, growthRate, periods, memo);
        }

        private decimal CalculateFVWithMemo(decimal presentValue, decimal growthRate, int periods, Dictionary<int, decimal> memo)
        {
            // Base case
            if (periods == 0)
                return presentValue;

            // Check if result is already computed
            if (memo.ContainsKey(periods))
            {
                Console.WriteLine($"  ⚡ Using memoized result for period {periods}");
                return memo[periods] * presentValue;
            }

            // Recursive calculation
            decimal result = CalculateFVWithMemo(presentValue, growthRate, periods - 1, memo) * (1 + growthRate);
            
            // Store result for future use
            memo[periods] = result / presentValue; // Store the multiplier
            return result;
        }

        // ============ TAIL RECURSION OPTIMIZATION ============
        
        /// <summary>
        /// Tail-recursive version - optimized for performance
        /// </summary>
        public decimal CalculateFutureValueTailRecursive(decimal presentValue, decimal growthRate, int periods)
        {
            return TailRecursiveHelper(presentValue, growthRate, periods, 1m);
        }

        private decimal TailRecursiveHelper(decimal currentValue, decimal growthRate, int remainingPeriods, decimal accumulatedMultiplier)
        {
            if (remainingPeriods == 0)
                return currentValue * accumulatedMultiplier;

            // Multiply accumulated multiplier by (1 + rate) each step
            return TailRecursiveHelper(currentValue, growthRate, remainingPeriods - 1, accumulatedMultiplier * (1 + growthRate));
        }

        // ============ ANNUITY CALCULATIONS ============
        
        /// <summary>
        /// Calculates future value of an annuity (regular payments)
        /// FV = PMT * [((1 + r)^n - 1) / r]
        /// </summary>
        public decimal CalculateAnnuityFutureValue(decimal payment, decimal growthRate, int periods)
        {
            if (growthRate == 0)
                return payment * periods;

            decimal compoundFactor = CalculateCompoundFactor(growthRate, periods);
            return payment * ((compoundFactor - 1) / growthRate);
        }

        private decimal CalculateCompoundFactor(decimal rate, int periods)
        {
            if (periods == 0)
                return 1m;
            
            return (1 + rate) * CalculateCompoundFactor(rate, periods - 1);
        }

        // ============ PREDICTIVE GROWTH BASED ON HISTORICAL DATA ============
        
        /// <summary>
        /// Calculates average growth rate from historical data
        /// </summary>
        public decimal CalculateAverageGrowthRate(decimal[] historicalValues)
        {
            if (historicalValues == null || historicalValues.Length < 2)
                return 0;

            decimal totalGrowthRate = 0;
            for (int i = 1; i < historicalValues.Length; i++)
            {
                decimal growthRate = (historicalValues[i] - historicalValues[i - 1]) / historicalValues[i - 1];
                totalGrowthRate += growthRate;
            }

            return totalGrowthRate / (historicalValues.Length - 1);
        }

        /// <summary>
        /// Predicts future values based on historical data using recursive method
        /// </summary>
        public List<PredictionResult> PredictFutureValues(
            decimal[] historicalData, 
            int predictionPeriods, 
            bool useMemoization = true)
        {
            var results = new List<PredictionResult>();
            
            if (historicalData == null || historicalData.Length == 0)
                return results;

            // Calculate average growth rate from historical data
            decimal averageGrowthRate = CalculateAverageGrowthRate(historicalData);
            decimal lastValue = historicalData[historicalData.Length - 1];

            Console.WriteLine($"Historical average growth rate: {averageGrowthRate:P2}");

            // Predict future values recursively
            var memo = new Dictionary<int, decimal>();
            for (int i = 1; i <= predictionPeriods; i++)
            {
                decimal predictedValue;
                
                if (useMemoization)
                {
                    predictedValue = CalculateFutureValueWithMemoization(lastValue, averageGrowthRate, i);
                }
                else
                {
                    predictedValue = CalculateFutureValueRecursive(lastValue, averageGrowthRate, i);
                }

                results.Add(new PredictionResult
                {
                    Period = i,
                    PredictedValue = predictedValue,
                    GrowthRate = averageGrowthRate,
                    IsMemoized = useMemoization
                });
            }

            return results;
        }

        // ============ SENSITIVITY ANALYSIS ============
        
        /// <summary>
        /// Performs sensitivity analysis for different growth rates
        /// </summary>
        public Dictionary<decimal, List<PredictionResult>> SensitivityAnalysis(
            decimal presentValue, 
            int periods, 
            decimal[] growthRates)
        {
            var results = new Dictionary<decimal, List<PredictionResult>>();

            foreach (var rate in growthRates)
            {
                var predictions = new List<PredictionResult>();
                for (int i = 1; i <= periods; i++)
                {
                    decimal value = CalculateFutureValueRecursive(presentValue, rate, i);
                    predictions.Add(new PredictionResult
                    {
                        Period = i,
                        PredictedValue = value,
                        GrowthRate = rate
                    });
                }
                results[rate] = predictions;
            }

            return results;
        }
    }

    /// <summary>
    /// Represents a prediction result
    /// </summary>
    public class PredictionResult
    {
        public int Period { get; set; }
        public decimal PredictedValue { get; set; }
        public decimal GrowthRate { get; set; }
        public bool IsMemoized { get; set; } = false;

        public override string ToString()
        {
            return $"Period {Period,2}: ${PredictedValue,12:F2} (Growth: {GrowthRate:P2}){(IsMemoized ? " ⚡" : "")}";
        }
    }
}