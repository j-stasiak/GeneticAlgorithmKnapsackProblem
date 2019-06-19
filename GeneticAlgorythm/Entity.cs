using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Entity
    { 
        public double Fitness { get; set; }
        public BitArray Genome { get; set; }

        public void Mutate(int[] indexes)
        {
            if (indexes.Any(i => i < 0 || i > Genome.Length))
            {
                throw new ArgumentException($"Bad indexes values. [{nameof(Mutate)}] method." );
            }

            foreach (var index in indexes)
            {
                Genome.Set(index, !Genome[index]);
            }
        }

        public int GetItemsValue(List<Item> items)
        {
            var sum = items.Select((t, i) => Convert.ToInt32(Genome[i]) * t.Value).Sum();

            return sum;
        }

        public int GetItemsWeight(List<Item> items)
        {
            var sum = items.Select((t, i) => Convert.ToInt32(Genome[i]) * t.Weight).Sum();

            return sum;
        }
    }

    public static class EntityExtensions
    {
        public static BitArray Concat(this BitArray first, BitArray second, int separationPoint)
        {
            var result = new BitArray(first.Length, false);

            for (var i = 0; i < separationPoint; ++i)
            {
                result[i] = first[i];
            }

            for (var j = separationPoint; j < second.Length; ++j)
            {
                result[j] = second[j];
            }

            return result;
        }
    }
}
