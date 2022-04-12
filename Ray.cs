namespace PhotorealisticRenderer
{
    public class Ray
    {
        public Vector3 origin;
        public Vector3 direction;
        public Ray(Vector3 origin, Vector3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalized;
        }
        public Vector3 Origin { get { return origin; } }
        public Vector3 Direction { get { return direction; } }
    }
}
