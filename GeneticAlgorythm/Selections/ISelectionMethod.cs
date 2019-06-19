namespace GeneticAlgorithm.Selections
{
    public interface ISelectionMethod
    {
        Population ConductSelection(Population population);
    }
}
