using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.GenClasses
{
    public class GenAlgorithm
    {
        public List<StudentsGroup> _StudentsGroups { get; }
        public List<Teacher> _Teachers { get; }
        public List<Subject> _Subjects { get; }
        public List<Auditorium> _Auditoriums { get; }

        public GenAlgorithm(List<StudentsGroup> studentsGroups, List<Teacher> teachers, List<Subject> subjects, List<Auditorium> auditoriums)
        {
            _StudentsGroups = studentsGroups;
            _Teachers = teachers;
            _Subjects = subjects;
            _Auditoriums = auditoriums;
        }

        private Chromosome TournamentSelection(List<Chromosome> population, int tournamentSize)
        {
            Random random = new Random();
            List<Chromosome> tournamentParticipants = new List<Chromosome>();
            for (int i = 0; i < tournamentSize; i++)
            {
                int randomIndex = random.Next(0, population.Count);
                tournamentParticipants.Add(population[randomIndex]);
            }

            Chromosome fittest = tournamentParticipants.OrderByDescending(chromosome => CalculateFitness(chromosome)).First();
            return fittest;
        }

        private void SinglePointCrossover(Chromosome parent1, Chromosome parent2, out Chromosome offspring1, out Chromosome offspring2)
        {
            Random random = new Random();
            int crossoverPoint = random.Next(0, parent1.Schedule.Count);

            offspring1 = new Chromosome();
            offspring2 = new Chromosome();

            offspring1.Schedule.AddRange(parent1.Schedule.Take(crossoverPoint).ToList());
            offspring1.Schedule.AddRange(parent2.Schedule.Skip(crossoverPoint).ToList());

            offspring2.Schedule.AddRange(parent2.Schedule.Take(crossoverPoint).ToList());
            offspring2.Schedule.AddRange(parent1.Schedule.Skip(crossoverPoint).ToList());
        }

        private void Mutation(List<Chromosome> chromosomes, double mutationRate)
        {
            Random random = new Random();

            foreach (Chromosome chromosome in chromosomes)
            {
                for (int i = 0; i < chromosome.Schedule.Count; i++)
                {
                    if (random.NextDouble() < mutationRate)
                    {
                        TimeSlot mutatedTimeSlot = chromosome.Schedule[i];
                        mutatedTimeSlot.Auditorium = GetRandomAuditorium();
                        mutatedTimeSlot.Subject = GetRandomSubject();
                        chromosome.Schedule[i] = mutatedTimeSlot;
                    }
                }
            }
        }

        private Subject GetRandomSubject()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, _Subjects.Count);
            return _Subjects[randomIndex];
        }

        private Auditorium GetRandomAuditorium()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, _Auditoriums.Count);
            return _Auditoriums[randomIndex];
        }

        private double CalculateFitness(Chromosome chromosome)
        {
            // Рассчитывать пригодность на основе определенных критериев
            // Вам необходимо определить собственные расчеты фитнеса в зависимости от ваших требований
            // Учитывайте ограничения, такие как максимальное количество студентов в аудитории и доступные часы для каждого предмета
            // Цель состоит в том, чтобы максимизировать функцию пригодности

            double fitness = 0.0;

            // Пример: Фитнес на основе количества уникальных предметов
            int uniqueSubjects = chromosome.Schedule.Select(slot => slot.Subject).Distinct().Count();
            fitness += uniqueSubjects;

            return fitness;
        }

        private List<Chromosome> InitializePopulation(int populationSize)
        {
            List<Chromosome> population = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                Chromosome chromosome = new Chromosome();
                foreach (var day in Enum.GetValues(typeof(DayOfWeekSchool)))
                {
                    for (int hour = 9; hour <= 13; hour++)
                    {
                        TimeSlot timeSlot = new TimeSlot
                        {
                            DayOfWeek = (int)day,
                            Hour = hour,
                            Auditorium = GetRandomAuditorium(),
                            Subject = GetRandomSubject()
                        };
                        chromosome.Schedule.Add(timeSlot);
                    }
                }
                population.Add(chromosome);
            }

            return population;
        }

        private void EvaluateFitness(List<Chromosome> population)
        {
            foreach (Chromosome chromosome in population)
            {
                double fitness = CalculateFitness(chromosome);
                chromosome.Fitness = fitness;
            }
        }

        private List<Chromosome> SelectionAndReproduction(List<Chromosome> population, int offspringCount)
        {
            List<Chromosome> offspring = new List<Chromosome>();
            int tournamentSize = 5;

            for (int i = 0; i < offspringCount; i++)
            {
                Chromosome parent1 = TournamentSelection(population, tournamentSize);
                Chromosome parent2 = TournamentSelection(population, tournamentSize);

                Chromosome child1, child2;
                SinglePointCrossover(parent1, parent2, out child1, out child2);

                offspring.Add(child1);
                offspring.Add(child2);
            }

            return offspring;
        }

        private void ReplaceAndUpdatePopulation(List<Chromosome> population, List<Chromosome> offspring)
        {
            population.Sort((chromosome1, chromosome2) => chromosome1.Fitness.CompareTo(chromosome2.Fitness));

            int offspringIndex = 0;
            for (int i = population.Count - offspring.Count; i < population.Count; i++)
            {
                population[i] = offspring[offspringIndex];
                offspringIndex++;
            }
        }

        public List<Chromosome> RunGeneticAlgorithm(int populationSize, int offspringCount, int maxGenerations, double mutationRate)
        {
            double fitnessThreshold = 50;
            List<Chromosome> population = InitializePopulation(populationSize);
            EvaluateFitness(population);

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                List<Chromosome> offspring = SelectionAndReproduction(population, offspringCount);
                Mutation(offspring, mutationRate);
                EvaluateFitness(offspring);
                ReplaceAndUpdatePopulation(population, offspring);

                if (ShouldTerminate(population, maxGenerations, fitnessThreshold))
                {
                    break;
                }
            }

            return population;
        }

        private bool ShouldTerminate(List<Chromosome> population, int maxGenerations, double fitnessThreshold)
        {
            return population.Any(chromosome => chromosome.Fitness >= fitnessThreshold) || population.Count >= maxGenerations;
        }


    }
}
