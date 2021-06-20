using System;
using System.Collections;
using System.Collections.Generic;
using DTO;

namespace GUI.HeredityObjects
{
    public class Chromosome
    {
        // Local variables
        private List<Gene> chromosomes;
        private Schedule schedule;

        // Constructors
        public Chromosome()
        {
            chromosomes = new List<Gene>();
            schedule = new Schedule();
        }
        public Chromosome(List<Gene> chromosomes, Schedule schedule)
        {
            this.chromosomes = new List<Gene>(chromosomes);
            this.schedule = new Schedule(schedule);
        }
        public Chromosome(Chromosome c)
        {
            chromosomes = new List<Gene>(c.chromosomes);
            schedule = new Schedule(c.Schedule);
        }

        // Property
        public int Length { get => chromosomes.Count; }
        public List<Gene> Genes { get => chromosomes; }
        public Schedule Schedule { get => new Schedule(schedule); }

        // Main method
        public void AddGene(string subject, string weekday, string classTime)
        {
            chromosomes.Add(new Gene(subject, weekday, classTime));
        }
        public void AddGene(Gene aGene)
        {
            chromosomes.Add(aGene);
        }
        public void RemoveGene(Gene g)
        {
            chromosomes.Remove(g);
        }
        public Gene GeneAt(int index)
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException();
            else return chromosomes[index];
        }
        public int GetFitness()
        {
            LoadSchedule();
            int fitness = 0;
            ArrayList coincidentClass = new ArrayList();
            for (int i = 0; i < Length; ++i)
            {
                if (!Time.ClassInFreetime(GeneAt(i).ClassTime))
                    continue;
                if (coincidentClass.Contains(i))
                    continue;
                fitness++;
                List<int> others = schedule.CoincidentClasses(i, GeneAt(i).ClassTime);
                if (others.Count == 0)
                    continue;
                else
                    foreach (var x in others)
                        coincidentClass.Add(x);
            }
            return fitness;
        }
        public void Sort()
        {
            chromosomes.Sort((g1, g2) =>
            {
                if (g1.ClassTime > g2.ClassTime)
                    return 1;
                if (g1.ClassTime == g2.ClassTime)
                    return 0;
                return -1;
            });
        }
        public Time GetClassTime(string subject)
        {
            foreach (var gene in chromosomes)
                if (gene.Subject == subject)
                    return gene.ClassTime;
            return null;
        }

        // Support method
        private void LoadSchedule()
        {
            schedule = new Schedule();
            for (int i = 0; i < Length; ++i)
                schedule.AddClass(i, GeneAt(i).ClassTime);
        }

        // Define operator ==, !=
        public static bool operator ==(Chromosome a, Chromosome b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            for (int i = 0; i < a.Length; ++i)
                if (a.GeneAt(i) != b.GeneAt(i))
                    return false;
            return true;
        }
        public static bool operator !=(Chromosome a, Chromosome b)
        {
            return !(a == b);
        }
    }
}
