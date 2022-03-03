using System;
namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Vector v = new Vector(1, 2, 3);
            Vector v1 = new Vector(4, 5, 6);
            Console.WriteLine(nameof(v) + " = "+ v + " distance of " + nameof(v) + " is " + v.distance());
            Console.WriteLine(nameof(v1) + " = "+ v1 + " distance of " + nameof(v1) + " is " + v1.distance());
        }
    }
}