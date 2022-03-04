using System;
namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Vector v = new Vector(1, 2, 3);
            Vector v1 = new Vector(4, 5, 6);
            Vector v2 = new Vector(v + v1);
            Vector v3 = new Vector(v - v1);
            v += v1;
            v2 -= v1;
            v1 *= v2;
            v3 /= v1;
            Console.WriteLine(nameof(v) + " = "+ v + " length of " + nameof(v) + " is " + v.length());
            Console.WriteLine(nameof(v1) + " = "+ v1 + " length of " + nameof(v1) + " is " + v1.length());
            Console.WriteLine(nameof(v2) + " = " + v2 + " length of " + nameof(v2) + " is " + v2.length());
            Console.WriteLine(nameof(v3) + " = " + v3 + " length of " + nameof(v3) + " is " + v3.length());
        }
    }
}