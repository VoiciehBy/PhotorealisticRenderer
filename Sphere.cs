using System;

namespace PhotorealisticRenderer.Shapes
{
    public class Sphere : Shape
    {
        public double Radius;

        public Sphere(Vector root, Vector eulerRotation, double Radius) : base(root, eulerRotation)
        {
            this.Radius = Radius;
        }

        public int CheckIntersection(Ray ray, out double dist1, out double dist2, bool print = true)
        {
            Vector o_minus_c = ray.origin - origin;

            double p = Vector.dotProductOf(ray.direction.Normalize(), o_minus_c);
            double q = Vector.dotProductOf(o_minus_c, o_minus_c) - (Radius * Radius);

            double discriminant = (p * p) - q;
            if (discriminant < 0.0f)
            {
                dist1 = 0;
                dist2 = 0;
                if (print) Console.WriteLine("No Intersections");
                return 0;
            }

            double dRoot = Math.Sqrt(discriminant);
            dist1 = -p - dRoot;
            dist2 = -p + dRoot;

            int amount = (discriminant > 1e-7) ? 2 : 1;
            if (print)
            {
                if (amount == 0) Console.WriteLine("No Intersections");
                else if (amount == 1) Console.WriteLine("One Intersection at " + (ray.origin + ray.direction.Normalize() * dist1).ToString());
                else
                {
                    Console.WriteLine("First Intersection at " + (ray.origin + ray.direction.Normalize() * dist1).ToString());
                    Console.WriteLine("Second Intersection at " + (ray.origin + ray.direction.Normalize() * dist2).ToString());
                }
            }
            return amount;
        }
    }
}