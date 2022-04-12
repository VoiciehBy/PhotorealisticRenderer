using PhotorealisticRenderer;

class PerspectiveCamera : Camera
{    
    Vector3 origin;
    double distance;
    Vector3 u;
    Vector3 v;
    Vector3 w;
    public PerspectiveCamera(Vector3 origin, Vector3 lookAt, Vector3 up, double distance)
    {
        this.origin = origin;
        this.distance = distance;
        w = origin - lookAt;
        w = w.Normalized;
        u = Vector3.Cross(up, w);
        u = u.Normalized;
        v = Vector3.Cross(w, u);
    }
    public override Ray GetRayTo(Vector2 relativeLocation)
    {
        return new Ray(origin, RayDirection(relativeLocation));
    }

    public Vector3 RayDirection(Vector2 vec)
    {
        return u * vec.X + v * vec.Y + w * -distance;
    }
}
