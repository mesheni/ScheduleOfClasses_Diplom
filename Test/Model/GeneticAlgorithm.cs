//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace Test.Model
//{
//    public class GeneticAlgorithm
//    {
//        private int populationSize;
//        private int elitismCount;
//        private double mutationRate;
//        private int tournamentSize;
//        private List<Subject> subjectList;
//        private List<double> fitnessWeights;

//        public GeneticAlgorithm(int populationSize, int elitismCount, double mutationRate, int tournamentSize,
//                                List<Subject> subjectList, List<double> fitnessWeights)
//        {
//            this.populationSize = populationSize;
//            this.elitismCount = elitismCount;
//            this.mutationRate = mutationRate;
//            this.tournamentSize = tournamentSize;
//            this.subjectList = subjectList;
//            this.fitnessWeights = fitnessWeights;
//        }

//        public List<Schedule> InitializePopulation()
//        {
//            var scheduleGenerator = new ScheduleGenerator(subjectList, 5); // Assuming 5 days per week
//            return scheduleGenerator.GenerateInitialPopulation(populationSize);
//        }

//        public List<Schedule> GenerateNextGeneration(List<Schedule> population)
//        {
//            var newPopulation = new List<Schedule>();

//            for (int i = 0; i < elitismCount; i++)
//            {
//                newPopulation.Add(population[i]);
//            }

//            while (newPopulation.Count < populationSize)
//            {
//                var parent1 = SelectParent(population);
//                var parent2 = SelectParent(population);
//                var child = Crossover(parent1, parent2);
//                if (RandomNumberGenerator.GetInt32(100) < mutationRate * 100)
//                {
//                    child = Mutate(child);
//                }
//                newPopulation.Add(child);
//            }

//            return newPopulation;
//        }

//        private Schedule SelectParent(List<Schedule> population)
//        {
//            var tournament = new List<Schedule>();
//            for (int i = 0; i < tournamentSize; i++)
//            {
//                tournament.Add(population[RandomNumberGenerator.GetInt32(population.Count)]);
//            }

//            return tournament.OrderByDescending(s => s.Fitness(fitnessWeights)).First();
//        }

//        private Schedule Crossover(Schedule parent1, Schedule parent2)
//        {
//            var child = new Schedule("Monday");

//            for (int i = 0; i < parent1.NameSubjects.Count; i++)
//            {
//                if (RandomNumberGenerator.GetInt32(2) == 0)
//                {
//                    child.NameSubjects.Add(parent1.NameSubjects[i]);
//                }
//                else
//                {
//                    child.NameSubjects.Add(parent2.NameSubjects[i]);
//                }

//                if ((i + 1) % 5 == 0) // Assuming 5 days per week
//                {
//                    child.DayOfWeek = GetNextDay(child.DayOfWeek);
//                }
//            }

//            return child;
//        }

//        private Schedule Mutate(Schedule schedule)
//        {
//            var availableSubjects = new List<Subject>(subjectList);
//            var dayIndex = RandomNumberGenerator.GetInt32(5); // Assuming 5 days per week
//            var subjectIndex = RandomNumberGenerator.GetInt32(availableSubjects.Count);
//            var selectedSubject = availableSubjects[subjectIndex];

//            var startIndex = dayIndex * 7; // Assuming 7 hours per day
//            var endIndex = startIndex + int.Parse(selectedSubject.CountHours);

//            for (int i = startIndex; i < endIndex; i++)
//            {
//                schedule.NameSubjects[i] = selectedSubject.NameSubject;
//            }

//            return schedule;
//        }

//        private string GetNextDay(string currentDay)
//        {
//            switch (currentDay)
//            {
//                case "Monday":
//                    return "Tuesday";
//                case "Tuesday":
//                    return "Wednesday";
//                case "Wednesday":
//                    return "Thursday";
//                case "Thursday":
//                    return "Friday";
//                case "Friday":
//                    return "Monday";
//                default:
//                    throw new ArgumentException("Invalid day of week");
//            }
//        }
//    }
//}
