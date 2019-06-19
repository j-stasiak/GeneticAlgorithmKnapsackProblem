using System;
using System.Collections;
using System.Collections.Generic;
using MoreLinq;

namespace GeneticAlgorithm
{
    public class Population
    {
        private readonly int _genomeLength;

        public List<Entity> Entities { get; }

        public Population(int entitiesNumber, int genomeLength)
        {
            _genomeLength = genomeLength;

            Entities = new List<Entity>(entitiesNumber);
        }

        public Population(List<Entity> entities)
        {
            Entities = entities;
        }

        public void Initialize()
        {
            var rand = new Random();

            for (var i = 0; i < Entities.Capacity; i++)
            {
                var entity = new Entity
                {
                    Genome = new BitArray(_genomeLength, false)
                };

                for (var j = 0; j < Math.Ceiling(_genomeLength / 3d); ++j)
                {
                    entity.Genome.Set(rand.Next(_genomeLength), true);
                }

                Entities.Add(entity);
            }
        }

        public Entity GetBestEntity()
        {
            return Entities.MaxBy(entity => entity.Fitness).First();
        }

        public (Entity FirstChild, Entity SecondChild) CrossoverEntities(Entity firstParent, Entity secondParent, int separationPoint)
        {
            var firstChild = new Entity();
            var secondChild = new Entity();

            firstChild.Genome = firstParent.Genome.Concat(secondParent.Genome, separationPoint);
            secondChild.Genome = secondParent.Genome.Concat(firstParent.Genome, separationPoint);

            return (firstChild, secondChild);
        }
    }
}
