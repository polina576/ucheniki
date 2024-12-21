using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uchen
{
    public class Student
    {
        public string LastName { get; set; }
        public string Initials { get; set; }
        public int Class { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
