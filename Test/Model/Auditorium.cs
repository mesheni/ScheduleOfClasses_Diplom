using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Auditorium
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfSeats { get; set; }

        public Subject subject { get; set; }
    }
}
