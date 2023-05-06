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
    }
}
