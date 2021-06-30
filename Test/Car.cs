using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Car
    {
        public int Wheels { get; set; }
        public string Name { get; set; }
        public List<string> Owners { get; set; }

        public Car(int wheels, string name)
        {
            Wheels = wheels;
            Name = name;
        }

        public Car()
        {
            
        }

        public override string ToString()
        {
            return $"Wheels: {Wheels} Name of car: {Name}" + $" Amount of Owners {Owners}";
        }
    }
}
