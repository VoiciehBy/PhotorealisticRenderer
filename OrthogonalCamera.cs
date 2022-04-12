using PhotorealisticRenderer;
using System;

class OrthogonalCamera : Camera
{
    public Vector3 origin;
    public double angle;
    public Vector2 size;

    public OrthogonalCamera(Vector3 origin, double angle, Vector2 size)
    {
        this.origin = origin;
        this.angle = angle;
        this.size = size;
    }

    public override Ray GetRayTo(Vector2 pictureLocation)
    {
        Vector3 direction = new(Math.Sin(angle), 0, Math.Cos(angle));
        direction = direction.Normalized;
        Vector2 offset = new(pictureLocation.X * size.X, pictureLocation.Y * size.Y);
        Vector3 position = new(origin.X + offset.X * Math.Cos(angle), origin.Y + offset.Y, origin.Z + offset.X * Math.Sin(angle));
        return new Ray(position, direction);
    }

}