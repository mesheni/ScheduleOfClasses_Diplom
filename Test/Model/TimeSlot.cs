using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class TimeSlot
    {
        public int DayOfWeek { get; set; } // День недели (1 - понедельник, 2 - вторник, ..., 6 - суббота)
        public int Hour { get; set; } // Час занятия (1 - 8:30, 2 - 10:15, 3 - 12:00, 4 - 13:45, 5 - 15:30, 6 - 17:15)
        public Auditorium Auditorium { get; set; } // Аудитория, в которой проходит занятие
        public Subject Subject { get; set; } // Предмет, который преподается
    }
}
