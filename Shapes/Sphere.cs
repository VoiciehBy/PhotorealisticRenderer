using System;

namespace PhotorealisticRenderer.Shapes
{
    class Sphere : Shape
    {
        Vector3 Origin;
        double Radius;
        public Sphere(Vector3 origin, double radius)
        {
            this.Origin = origin;
            this.Radius = radius;
        }

        public override bool CheckIntersection(Ray ray, ref double minDistance)
        {
            double t;
            Vector3 distance = ray.Origin - Origin;
            double a = ray.Direction.LengthSq;
            double b = (distance * 2).Dot(ray.Direction);
            double c = distance.LengthSq - Radius * Radius;
            double disc = b * b - 4 * a * c;
            if (disc < 0) { return false; }
            double discSq = Math.Sqrt(disc);
            double denom = 2 * a;
            t = (-b - discSq) / denom;
            if (t < 0.0001)
            { t = (-b + discSq) / denom; }
            if (t < 0.0001)
            { return false; }
            Vector3 hitPoint = (ray.Origin + ray.Direction * t);
            minDistance = t;

            return true;
        }

    }
}