using System;

namespace PhotorealisticRenderer.Shapes
{
    public class Plane : Shape
    {
        Vector3 origin;
        public Vector3 direction;
        public Plane(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }
        public override bool CheckIntersection(Ray ray, ref double distance, out Vector3 normal)
        {
            double t = (origin - ray.Origin).Dot(direction) / ray.Direction.Dot(direction);
            if (t > 0.00001)
            {
                distance = t;
                normal = direction;
                return true;
            }

            normal = default;
            return false;
        }

    }
}
