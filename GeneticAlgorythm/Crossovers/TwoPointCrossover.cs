using System;

namespace GeneticAlgorithm.Crossovers
{
    public class TwoPointCrossover : ICrossoverMethod
    {
        public double CrossoverChance { get; set; } = 1;

        public Population Crossover(Population population)
        {
            throw new NotImplementedException();
        }
    }
}
