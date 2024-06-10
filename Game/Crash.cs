using MathNet.Numerics.Distributions;
using System.Collections.Concurrent;

namespace UNSERcasino.game
{
    internal class Crash
    {
        public double Play()
        {
            double expectedMultiplier = 2; // 2x multiplier

            double houseAdvantage = 0.02;

            double adjustedMultiplier = expectedMultiplier * (1 - houseAdvantage); // 2% house advantage
            var rate = 1.0 / adjustedMultiplier;
            var exponentialDistribution = new Exponential(rate); // Exponential distribution with rate 1/adjustedMultiplier
            Random random = new Random();

            double crashchance = random.NextDouble();

            double randomNumber = exponentialDistribution.Sample() + 1;
            if (crashchance <= 0.01)
            {
                randomNumber = 1;
            }
            //Console.WriteLine($"Random number (adjusted): {randomNumber}");
            return randomNumber;
        }

        public void TestAverage() // Test the average of the game
        {
            int runs = 500000;
            double total = 0;
            int counter = 0;
            object lockObject = new object();

            var rangePartitioner = Partitioner.Create(0, runs); // Partition the range of runs

            Parallel.ForEach(rangePartitioner, (range, loopState) =>  
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var result = Play();
                    lock (lockObject) // Lock the total variable, so that no other thread can access it
                    {
                        total += result;
                        counter++;
                        Console.WriteLine($"Run {counter}: Current Average = {total / counter}");
                    }
                }
            });

            Console.WriteLine($"Final Average after {runs} runs: {total / runs}"); // Output the final average
            Console.ReadKey();
        }


    }
}

