using System;

namespace PhotorealisticRenderer.Shapes;

public class Triangle : Shape
{
    public Vector3 A { get; private set; }
    public Vector3 B { get; private set; }
    public Vector3 C { get; private set; }

    public Vector3 NA { get; private set; }
    public Vector3 NB { get; private set; }
    public Vector3 NC { get; private set; }

    public Plane plane;


    public Triangle(Vector3 a, Vector3 b, Vector3 c) => SetPosition(a, b, c);

    public Triangle SetPosition(Vector3 a, Vector3 b, Vector3 c)
    {
        A = a;
        B = b;
        C = c;
        Vector3 normal = Vector3.Cross(b - a, b - c).Normalized;
        plane = new Plane(a, normal);

        return this;
    }

    public Triangle SetNormals(Vector3 na, Vector3 nb, Vector3 nc)
    {
        NA = na;
        NB = nb;
        NC = nc;

        return this;
    }

    public override bool CheckIntersection(Ray ray, ref double distance, out Vector3 normal) //Moller-Trumbone
    {
        if (!plane.CheckIntersection(ray, ref distance, out normal))
        {
            return false;
        }

        double kEpsilon = 0.000001;
        Vector3 v0v1 = B - A;
        Vector3 v0v2 = C - A;
        var innerNormal = (Vector3.Cross(B - A, B - C)).Normalized;
        Vector3 h = Vector3.Cross(ray.Direction, v0v2);
        double a = v0v1.Dot(h);
        double d = innerNormal.Dot(A);
        double t = -(innerNormal.Dot(ray.Origin) + d) / innerNormal.Dot(ray.Direction);
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
        if (this is { NA: { }, NB: { }, NC: { } }) // Null check for NA/NB/NC
            normal = (NA * (1 - u - v) + NB * u + NC * v).Normalized;
        return true;
    }
}