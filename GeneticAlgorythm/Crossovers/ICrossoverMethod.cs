namespace GeneticAlgorithm.Crossovers
{
    public interface ICrossoverMethod
    {
        double CrossoverChance { get; set; }
        Population Crossover(Population population);
    }
}
