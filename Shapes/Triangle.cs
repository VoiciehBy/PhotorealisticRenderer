using System;

namespace PhotorealisticRenderer.Shapes;

public class Triangle : Shape
{
    public Vector3 A { get; private set; }
    public Vector3 B { get; private set; }
    public Vector3 C { get; private set; }
    public Vector3 Normal;
    public Plane plane;


    public Triangle(Vector3 A, Vector3 B, Vector3 C)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        Vector3 normal = (Vector3.Cross(B - A, B - C)).Normalized;
        plane = new Plane(A, normal);
    }

    public override bool CheckIntersection(Ray ray, ref double distance) //Moller-Trumbone
    {
        if (!plane.CheckIntersection(ray, ref distance))
        {
            return false;
        }
        double kEpsilon = 0.000001;
        Vector3 v0v1 = B - A;
        Vector3 v0v2 = C - A;
        Normal = (Vector3.Cross(B - A, B - C)).Normalized;
        Vector3 h = Vector3.Cross(ray.Direction, v0v2);
        double a = v0v1.Dot(h);
        double d = Normal.Dot(A);
        double t = -(Normal.Dot(ray.Origin) + d) / Normal.Dot(ray.Direction);
        if (a <= kEpsilon) return false;
        if (Math.Abs((a)) <= kEpsilon) return false;
        double f = 1 / a;
        Vector3 tvec = ray.Origin - A;
        double u = tvec.Dot(h) * f;
        if (u <= 0 || u >= 1) return false;
        Vector3 qvec = Vector3.Cross(tvec, v0v1);
        double v = ray.Direction.Dot(qvec) * f;
        if (v <= 0 || u + v >= 1) return false;
        distance = t;
        return true;
    }

}