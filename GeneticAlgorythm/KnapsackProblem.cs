using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticAlgorithm.Crossovers;
using GeneticAlgorithm.Selections;

namespace GeneticAlgorithm
{
    public class KnapsackProblem
    {
        private List<Population> _populations;
        private readonly int _iterations;
        private readonly List<Item> _items;
        
        private readonly int _backpackCapacity;

        public List<double> BestEntities { get; }
        public List<double> BestHistoryEntities { get; }
        public List<double> MeanPopulation { get; }
        public ISelectionMethod SelectionMethod { get; set; }
        public IMutationMethod MutationMethod { get; set; }
        public ICrossoverMethod CrossoverMethod { get; set; }

        public KnapsackProblem(Population initialPopulation, int backpackCapacity, int iterations, List<Item> items)
        {
            _backpackCapacity = backpackCapacity;
            _items = items;
            _iterations = iterations;

            BestEntities = new List<double>(iterations);
            BestHistoryEntities = new List<double>(iterations);
            MeanPopulation = new List<double>(iterations);

            initialPopulation.Initialize();

            _populations = new List<Population>(iterations)
            {
                initialPopulation
            };
        }

        public void Run()
        {
            Console.WriteLine("GA is running...");

            for (var i = 0; i < _iterations; i++)
            {
                CalculateFitness(_populations[i]);

                BestEntities.Add(_populations[i].GetBestEntity().Fitness);
                BestHistoryEntities.Add(BestEntities.Max());
                MeanPopulation.Add(_populations[i].Entities.Select(x => x.Fitness).Sum() / _populations[i].Entities.Count);

                var newPopulation = SelectionMethod.ConductSelection(_populations[i]);
                newPopulation = CrossoverMethod.Crossover(newPopulation);
                newPopulation = MutationMethod.Mutate(newPopulation);
                _populations.Add(newPopulation);
            }
           
            Console.WriteLine("Finished");
        }

        public void ExportResults(string fileName)
        {
            using (var stream = new StreamWriter(fileName))
            {
                stream.WriteLine("Population; Best Entities; Best History Entities; Mean Population");
                for (var i = 0; i < BestEntities.Count; i++)
                {
                    stream.WriteLine($"{i + 1}; {BestEntities[i]}; {BestHistoryEntities[i]}; {MeanPopulation[i]}");
                }
            }
        }

        private void CalculateFitness(Population currentPopulation)
        {
            foreach (var entity in currentPopulation.Entities)
            {
                var itemsWeight = entity.GetItemsWeight(_items);

                entity.Fitness = itemsWeight <= _backpackCapacity ? entity.GetItemsValue(_items) : 0;
            }
        }
    }
}
