//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace Test.Model
//{
//    public class ScheduleGenerator
//    {
//        private List<Subject> subjectList;
//        private int daysPerWeek;

//        public ScheduleGenerator(List<Subject> subjectList, int daysPerWeek)
//        {
//            this.subjectList = subjectList;
//            this.daysPerWeek = daysPerWeek;
//        }

//        public List<Schedule> GenerateInitialPopulation(int populationSize)
//        {
//            var schedules = new List<Schedule>();

//            for (int i = 0; i < populationSize; i++)
//            {
//                var schedule = new Schedule("Monday");
//                var availableSubjects = new List<Subject>(subjectList);

//                for (int j = 0; j < daysPerWeek; j++)
//                {
//                    var subjectIndex = RandomNumberGenerator.GetInt32(availableSubjects.Count);
//                    var selectedSubject = availableSubjects[subjectIndex];

//                    for (int k = 0; k < int.Parse(selectedSubject.CountHours); k++)
//                    {
//                        schedule.NameSubjects.Add(selectedSubject.NameSubject);
//                    }

//                    availableSubjects.RemoveAt(subjectIndex);
//                    if (availableSubjects.Count == 0)
//                    {
//                        availableSubjects = new List<Subject>(subjectList);
//                    }

//                    schedule.DayOfWeek = GetNextDay(schedule.DayOfWeek);
//                }

//                schedules.Add(schedule);
//            }

//            return schedules;
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
