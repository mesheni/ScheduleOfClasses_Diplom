using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    // Класс предмета
    public class Subject
    {
        public string NameSubject { get; set; } // Название предмета
        public int CountHours { get; set; } // Количество часов в неделю
        public int Complexity { get; set; } // Сложность предмета
    }
}
