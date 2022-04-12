using System.Collections.Generic;
using PhotorealisticRenderer.Shapes;

namespace PhotorealisticRenderer
{
    public class Scene
    {
        public LightIntensity BackgroundColor = new(0, 0, 0);
        public List<Shape> Shapes = new();

        public Shape TraceRay(Ray ray)
        {
            Shape? shape = null;
            double minimalDistance = double.MaxValue;
            double lastDistance = 0;

            foreach (var obj in Shapes)
            {
                if (obj.CheckIntersection(ray, ref lastDistance) && lastDistance < minimalDistance)
                {
                    minimalDistance = lastDistance;
                    shape = obj;
                }
            }
            return shape;
        }
    }
}