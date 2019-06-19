using System;

namespace GeneticAlgorithm.Mutations
{
    public class FlipBitMutation : IMutationMethod
    {
        public double MutationChance { get; set; } = 0.05;

        public Population Mutate(Population population)
        {
            var random = new Random();

            for (var i = 0; i < population.Entities.Count; i++)
            {
                if (random.NextDouble() > MutationChance) continue;

                var mutationBits = random.Next(1, (int) Math.Sqrt(population.Entities[0].Genome.Count));
                var tab = new int[mutationBits];

                for (var k = 0; k < tab.Length; k++)
                {
                    tab[k] = random.Next(population.Entities[0].Genome.Count);
                }

                population.Entities[i].Mutate(tab);

            }

            return population;
        }
    }
}
