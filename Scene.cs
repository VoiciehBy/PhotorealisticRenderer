using System.Collections.Generic;
using PhotorealisticRenderer.Shapes;

namespace PhotorealisticRenderer
{
    public class Scene
    {
        public Intensity BackgroundColor { get; set; } = new(0, 0, 0);
        public List<Sphere> Spheres { get; } = new();
        public List<Plane> Planes { get; } = new();

        public Intensity GetClosestIntersection(in Ray ray, in float nearPlane, in float farPlane)
        {
            var currentDistance = farPlane + double.Epsilon;
            Intensity currentIntensity = null;

            foreach (var sphere in Spheres)
            {
                if (sphere.color == null || sphere.CheckIntersection(ray, out var dist1, out var dist2, false) == 0)
                    continue;

                if (dist1 < currentDistance && dist1 <= farPlane && dist1 >= nearPlane)
                {
                    currentDistance = dist1;
                    currentIntensity = sphere.color;
                }

                if (dist2 < currentDistance && dist2 <= farPlane && dist2 >= nearPlane)
                {
                    currentDistance = dist2;
                    currentIntensity = sphere.color;
                }
            }

            foreach (var plane in Planes)
            {
                if (!plane.CheckIntersection(ray, out var dist, false))
                    continue;

                if (dist < currentDistance && dist <= farPlane && dist >= nearPlane)
                {
                    currentDistance = dist;
                    // TODO: Assign currentIntensity
                }
            }
            
            return currentIntensity;
        }
    }
}