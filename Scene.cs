using System.Collections.Generic;
using PhotorealisticRenderer.Shapes;

namespace PhotorealisticRenderer
{
    public class Scene
    {
        public LightIntensity BackgroundColor = LightIntensity.Black;
        public List<Shape> Shapes = new();
        public List<PointLight> Lights = new();

        public RayHit TraceRay(Ray ray)
        {
            RayHit result = new RayHit();
            Vector3 normal;
            double minimalDistance = double.MaxValue; // najbliższe trafienie
            double lastDistance = 0; // zmienna pomocnicza, ostatnia odległość

            foreach (var obj in Shapes)
            {
                if (obj.CheckIntersection(ray, ref lastDistance, out normal) && lastDistance < minimalDistance) // jeśli najbliższe trafienie
                {
                    minimalDistance = lastDistance; // nowa najmniejsza odległość
                    result.Hit = obj; // nowy trafiony obiekt
                    result.Normal = normal; // normalna trafienia
                }
            }

            if (result.Hit != null) // jeśli trafiliśmy cokolwiek
            {
                result.HitPoint = ray.Origin + ray.Direction * minimalDistance;
                result.Ray = ray;
                result.Scene = this;
            }
            return result;
        }

        public Shape TraceRay(in Ray ray, out double distance, out Vector3 normal)
        {
            Shape shape = null;
            normal = default;
            distance = double.MaxValue;
            double lastDistance = 0;

            foreach (var obj in Shapes)
            {
                if (obj.CheckIntersection(ray, ref lastDistance, out var newNormal) && lastDistance < distance)
                {
                    distance = lastDistance;
                    shape = obj;
                    normal = newNormal;
                }
            }
            return shape;
        }

        public bool CheckBetween(Vector3 pointA, Vector3 pointB)
        {
            Vector3 vectorAB = pointB - pointA;
            double distAB = vectorAB.Length;
            double currDistance = double.MaxValue;
            Ray ray = new Ray(pointA, vectorAB);
            foreach (var obj in Shapes)
            {
                if (obj.CheckIntersection(ray, ref currDistance, out _) && currDistance < distAB)
                { return true; }
            }
            return false;
        }
    }
}