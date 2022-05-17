using PhotorealisticRenderer.Shapes;

namespace PhotorealisticRenderer
{
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction.Normalized;
        }
        public Vector3 Origin;
        public Vector3 Direction;
    }

    public class RayHit
    {
        public Shape Hit { get; set; }
        public Scene Scene { get; set; }
        public Vector3 Normal { get; set; }
        public Vector3 HitPoint { get; set; }
        public Ray Ray { get; set; }
    }
}
