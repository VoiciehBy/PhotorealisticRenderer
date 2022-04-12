namespace PhotorealisticRenderer.Shapes
{
    public abstract class Shape
    {
        public LightIntensity Color;
        public abstract bool CheckIntersection(Ray ray, ref double distance);
    }
}
