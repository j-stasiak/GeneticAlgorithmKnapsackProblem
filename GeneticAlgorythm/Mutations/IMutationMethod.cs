namespace GeneticAlgorithm
{
    public interface IMutationMethod
    {
        double MutationChance { get; set; }
        Population Mutate(Population population);
    }
}
