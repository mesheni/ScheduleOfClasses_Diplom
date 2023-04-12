using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Schedule
    {
        public string DayOfWeek { get; set; }
        public List<string> NameSubjects = new List<string>();

        public Schedule(string day, List<string> nameSubject)
        {
            DayOfWeek = day;
            NameSubjects.AddRange(nameSubject);
        }

        public double Fitness(List<double> fitnessWeights)
        {
            var subjectCounts = new Dictionary<string, int>();
            var score = 0.0;

            // Calculate number of hours per subject
            foreach (var subject in NameSubjects)
            {
                if (string.IsNullOrEmpty(subject))
                {
                    continue;
                }

                if (!subjectCounts.ContainsKey(subject))
                {
                    subjectCounts[subject] = 1;
                }
                else
                {
                    subjectCounts[subject]++;
                }
            }

            // Calculate score based on fitness weights
            foreach (var weight in fitnessWeights)
            {
                switch (weight)
                {
                    case 1:
                        // Penalty for having two consecutive hours of the same subject
                        for (int i = 0; i < NameSubjects.Count - 1; i++)
                        {
                            if (NameSubjects[i] == NameSubjects[i + 1] && !string.IsNullOrEmpty(NameSubjects[i]))
                            {
                                score -= weight;
                            }
                        }
                        break;
                    case 2:
                        // Bonus for having a subject spread out evenly throughout the week
                        foreach (var subject in subjectCounts)
                        {
                            if (subject.Value < 4 || subject.Value > 6) // Assuming 5 days per week
                            {
                                score -= weight;
                            }
                        }
                        break;
                    case 3:
                        // Penalty for having more than 5 hours of the same subject per day
                        for (int i = 0; i < 5; i++) // Assuming 5 days per week
                        {
                            var subjectCountsPerDay = new Dictionary<string, int>();
                            for (int j = i * 7; j < (i + 1) * 7; j++)
                            {
                                if (!string.IsNullOrEmpty(NameSubjects[j]))
                                {
                                    if (!subjectCountsPerDay.ContainsKey(NameSubjects[j]))
                                    {
                                        subjectCountsPerDay[NameSubjects[j]] = 1;
                                    }
                                    else
                                    {
                                        subjectCountsPerDay[NameSubjects[j]]++;
                                    }
                                }
                            }

                            foreach (var count in subjectCountsPerDay.Values)
                            {
                                if (count > 5)
                                {
                                    score -= weight;
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid fitness weight");
                }
            }

            return score;
        }
    }
}
