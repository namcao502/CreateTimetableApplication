using System;
using System.Collections.Generic;
using BLL;
using DTO;

namespace GUI.HeredityObjects
{
    public class Heredity
    {
        // Global variables
        public static string[] subjects;
        public static Dictionary<string, List<Time>> availableTimesOfSubject;
        public static BusinessLogic bll = new BusinessLogic();
        public static Random rand = new Random();
        public static int numberChrome = 20;

        // Main method
        public static Population Initialize(List<string> subjects, List<Time> possibleTimes)
        {
            Setup(subjects, possibleTimes);
            Population population = new Population();
            for (int amount = 0; amount < numberChrome; ++amount)
            {
                Chromosome chrome = new Chromosome();
                foreach (string subject in subjects)
                {
                    int length = availableTimesOfSubject[subject].Count;
                    Time classTime = availableTimesOfSubject[subject][rand.Next(length)];
                    chrome.AddGene(new Gene(subject, classTime));
                }
                population.AddChromosome(chrome);
            }
            return population;
        }
        public static Population Selection(Population oldPopulation)
        {
            Population newPopulation = new Population();
            int[] fitnesses = oldPopulation.Fitnesses();
            int sum = 0;
            for (int i = 0; i < fitnesses.Length; ++i)
                sum += fitnesses[i];
            if (sum == 0)
            {
                for (int i = 0; i < numberChrome; ++i)
                    newPopulation.AddChromosome(new Chromosome(oldPopulation.ChromosomeAt(rand.Next(numberChrome))));
                return newPopulation;
            }
            for (int i = 0; i < numberChrome; ++i)
            {
                int temp = rand.Next(sum) + 1;
                for (int j = 0, temp2 = 0; j < numberChrome; ++j)
                {
                    temp2 += fitnesses[j];
                    if (temp2 >= temp)
                    {
                        newPopulation.AddChromosome(new Chromosome(oldPopulation.ChromosomeAt(j)));
                        break;
                    }
                }
            }
            return newPopulation;
        }
        public static Population Crossover(Population oldPopulation)
        {
            Population newPopulation = new Population();
            for (int i = 0; i < numberChrome; i += 2)
            {
                Chromosome father = oldPopulation.ChromosomeAt(i), mother = oldPopulation.ChromosomeAt(i + 1);
                Chromosome child1 = new Chromosome(), child2 = new Chromosome();
                for (int j = 0; j < father.Length; ++j)
                {
                    if (rand.Next(2) == 1)
                    {
                        child1.AddGene(new Gene(father.GeneAt(j)));
                        child2.AddGene(new Gene(mother.GeneAt(j)));
                    }
                    else
                    {
                        child1.AddGene(new Gene(mother.GeneAt(j)));
                        child2.AddGene(new Gene(father.GeneAt(j)));
                    }
                }
                newPopulation.AddChromosome(child1, child2);
            }
            return newPopulation;
        }
        public static void Mutation(Population population)
        {
            for (int i = 0, numberGene = population.ChromosomeAt(0).Length; i < numberChrome; ++i)
                for (int j = 0; j < numberGene; ++j)
                {
                    if (rand.Next(numberGene) > 0)
                        continue;
                    Gene gene = population.ChromosomeAt(i).GeneAt(j);
                    int length = availableTimesOfSubject[gene.Subject].Count;
                    Time classTime = availableTimesOfSubject[gene.Subject][rand.Next(length)];
                    gene.ClassTime = classTime;
                }
        }

        // Support methods
        public static void Setup(List<string> sj, List<Time> pt)
        {
            subjects = sj.ToArray();
            availableTimesOfSubject = new Dictionary<string, List<Time>>();
            foreach (string subject in subjects)
                availableTimesOfSubject.Add(subject, bll.AvailableTimeOfSubject(subject));
            Schedule.possibleTimes = pt.ToArray();
        }
    }
}
