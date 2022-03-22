using PhotorealisticRenderer.Shapes;
using System;
namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Sphere sphere = new Sphere(new Vector(0, 0, 0), new Vector(0, 0, 0), 10);
            Ray ray = new Ray(new Vector(0, 0, -20), new Vector(0, 0, 1));
            Ray ray2 = new Ray(new Vector(0, 0, -20), new Vector(0, 1, 0));
            Ray ray3 = new Ray(new Vector(0, 10, 0), new Vector(1, 0, 0));
            Plane plane = new Plane(new Vector(0, 0, 0), new Vector(0, 0.5, 0.5));

            double dist2; double dist1;
            int one = sphere.CheckIntersection(ray, out dist1, out dist2);

            double dist3; double dist4;
            int two = sphere.CheckIntersection(ray2, out dist3, out dist4);

            double dist5; double dist6;
            int three = sphere.CheckIntersection(ray3, out dist5, out dist6);

            double dist7;
            bool four = plane.CheckIntersection(ray2, out dist7);

            return;
        }


    }
}