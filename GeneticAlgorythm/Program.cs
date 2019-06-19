using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Crossovers;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Selections;

namespace GeneticAlgorithm
{
    public class Program
    {
        private static void Main()
        {
            if (!int.TryParse(Console.ReadLine(), out var itemsNumber))
            {
                Console.WriteLine("Value must be int!");
                return;
            }

            var random = new Random();
            var items = new List<Item>(itemsNumber);
            var backpackWeightCapacity = itemsNumber * 10;

            for (var i = 0; i < items.Capacity; i++)
            {
                items.Add(new Item
                {
                    Value = random.Next(1, random.Next(1, itemsNumber*2)),
                    Weight = random.Next(1, random.Next(1, itemsNumber*2))
                });
            }

            var problem = new KnapsackProblem(new Population(100, itemsNumber),
                backpackWeightCapacity, 1000, items)
            {
                SelectionMethod = new RouletteSelection(),
                CrossoverMethod = new SinglePointCrossover(),
                MutationMethod = new FlipBitMutation()
            };

            problem.Run();

            Console.WriteLine($"Max value: {items.Select(x => x.Value).Sum()}");
            Console.WriteLine($"Max weight: {items.Select(x => x.Weight).Sum()}");
            problem.ExportResults("results.csv");

            Console.ReadLine();
        }
    }
}
