using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.GenClasses
{
    public class Chromosome
    {
        public List<TimeSlot> Schedule { get; set; } // Расписание
        public double Fitness { get; internal set; }

        public Chromosome()
        {
            Schedule = new List<TimeSlot>();
        }
    }
}
