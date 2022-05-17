namespace PhotorealisticRenderer.Shapes
{
    public abstract class Shape
    {
        public PhongMaterialBase Material;

        public bool CheckIntersection(Ray ray, ref double distance) => CheckIntersection(ray, ref distance, out _);
        public abstract bool CheckIntersection(Ray ray, ref double distance, out Vector3 normal);
    }
}
