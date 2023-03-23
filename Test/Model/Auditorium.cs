using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Auditorium
    {

        public Auditorium() { }
        //public Auditorium(string num, string subj, string cap, string build) 
        //{
        //    Number = num;
        //    Subject = subj;
        //    Capacity = cap;
        //    Building = build;
        //}


        public string Number { get; set; } // Номер кабинета
        public string Subject { get; set; } // Предмет
        public string Capacity { get; set; } // Вместимость
        public string Building { get; set; } // Здание
    }
}
