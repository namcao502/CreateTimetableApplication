using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.HeredityObjects
{
    public class Population
    {
        // Local variables
        private List<Chromosome> population;

        // Constructor
        public Population()
        {
            population = new List<Chromosome>();
        }
        public Population(Population p)
        {
            population = new List<Chromosome>(p.population);
        }

        // Property
        public int Length { get => population.Count; }
        public List<Chromosome> Chromosomes { get => population; }

        // Main methods
        public void AddChromosome(Chromosome chromosome)
        {
            population.Add(chromosome);
        }
        public void AddChromosome(params Chromosome[] chromosomes)
        {
            foreach (var chrome in chromosomes)
                population.Add(chrome);
        }
        public Chromosome ChromosomeAt(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException();
            else return population[index];
        }
        public int[] Fitnesses()
        {
            int[] fitnesses = new int[Length];
            for (int i = 0; i < Length; ++i)
                fitnesses[i] = ChromosomeAt(i).GetFitness();
            return fitnesses;
        }
    }
}
