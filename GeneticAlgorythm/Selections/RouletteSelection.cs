using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.Selections
{
    public class RouletteSelection : ISelectionMethod
    {
        public Population ConductSelection(Population population)
        {
            var fitnessSum = population.Entities.Select(x => x.Fitness).Sum();
            var newEntities = new List<Entity>();
            var random = new Random();

            var rouletteWheel = population.Entities
                .Select((entity, i) => new KeyValuePair<int, double>(i, entity.Fitness / fitnessSum))
                .Where(kvp => kvp.Value != 0)
                .OrderByDescending(pair => pair.Value)
                .ToList();

            while (newEntities.Count < population.Entities.Count)
            {
                var num = random.NextDouble();
                var valueSum = 0d;
                var iter = 0;

                for (; valueSum < num; ++iter)
                {
                    valueSum += rouletteWheel[iter == rouletteWheel.Count ? iter - 1 : iter].Value;
                }

                newEntities.Add(population.Entities[rouletteWheel[iter == rouletteWheel.Count ? iter - 1 : iter].Key]);
            }

            return new Population(newEntities);
        }
    }
}
