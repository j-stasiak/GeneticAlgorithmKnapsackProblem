using System;

namespace GeneticAlgorithm.Crossovers
{
    public class SinglePointCrossover : ICrossoverMethod
    {
        public double CrossoverChance { get; set; } = 1;

        public Population Crossover(Population population)
        {
            var random = new Random();
            var parentsEntities = new Entity[2];

            for (var i = 0; i < population.Entities.Count / 2; i++)
            {
                var firstParentIndex = random.Next(population.Entities.Count);
                var secondParentIndex = random.Next(population.Entities.Count);

                parentsEntities[0] = population.Entities[firstParentIndex];
                parentsEntities[1] = population.Entities[secondParentIndex];

                if (random.NextDouble() <= CrossoverChance)
                {
                    (parentsEntities[0], parentsEntities[1]) = population.CrossoverEntities(parentsEntities[0],
                        parentsEntities[1], random.Next(parentsEntities[0].Genome.Count));
                }

                population.Entities[firstParentIndex] = parentsEntities[0];
                population.Entities[secondParentIndex] = parentsEntities[1];
            }

            return population;
        }
    }
}
