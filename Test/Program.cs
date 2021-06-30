using System;
using System.Collections.Generic;
using JsonSerializer;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car(4, "Volvo");
            Car car1 = new Car() {Name = "Ford", Owners = new List<string>() {"Søren", "Claus", "Jens"}, Wheels = 6};
            string json = "{\"Wheels\":4,\"Name\":\"Volvo\"}";

            Console.WriteLine((json.Deserialize<Car>()));
            Console.WriteLine();
            //Console.WriteLine(car.Serialize());
            Console.WriteLine();
            Console.WriteLine(car1.Serialize());

            //foreach (var item in car1.Owners)
            //{
            //    Console.WriteLine(item);
            //}



        }
    }
}
