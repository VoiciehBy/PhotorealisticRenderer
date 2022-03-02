using System;
namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Point A = new Point(1, 2, 3);
            Point B = new Point(4, 5, 6);
            Vector AB = new Vector(A, B);
            Console.WriteLine(nameof(AB) + AB + "distance of " + nameof(AB) + " is " + AB.distance());
        }
    }
}