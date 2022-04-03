using System;

namespace PhotorealisticRenderer.Shapes
{
    public class Plane
    {
        Vector origin;
        Vector direction;

        public Plane(Vector origin, Vector direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public bool CheckIntersection(Ray ray, out double dist, bool print = true)
        {

            double denom = Vector.dotProductOf(direction, ray.direction);
            if (denom > 1e-6)
            {
                Vector p0l0 = origin - ray.origin;
                dist = Vector.dotProductOf(p0l0, direction) / denom;
                if (dist >= 0)
                {
                    if (print) Console.WriteLine("One Intersection at " + (ray.origin + ray.direction.Normalize() * dist).ToString());
                    return true;
                }
            }
            else if (denom == 0)
            {

            }
            dist = -1;
            if (print) Console.WriteLine("No Intersection");
            return false;
        }
    }
}
