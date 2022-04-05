using System.Drawing;

namespace PhotorealisticRenderer;

public abstract class Camera
{
    // All the vectors will need custom code set up to recalculate the changes to the remaining ones if something changes
    public Vector Position { get; }
    public Vector Target { get; }
    public Vector Forward { get; }
    public Vector Up { get; }
    public Vector Right { get; }
    public float NearPlane { get; set; }
    public float FarPlane { get; set; }
    public AntiAliasing AntiAliasing { get; set; } = AntiAliasing.None;
    // public float Fov { get; set; }

    protected Camera()
    {
        Position = new Vector(0, 0, 0);
        Target = new Vector(0, 0, 1);
        Forward = new Vector(0, 0, 1);
        NearPlane = 1;
        FarPlane = 1000;
        Up = new Vector(0, 1, 0);
        Right = -Vector.crossProductOf(Forward, Up).Normalize();
    }

    protected Camera(Vector position, Vector target)
    {
        Position = position;
        Target = target;
        Forward = (Target - Position).Normalize();
        NearPlane = 1;
        FarPlane = 1000;
        Up = new Vector(0, 1, 0);
        Right = -Vector.crossProductOf(Forward, Up).Normalize();
    }

    public abstract void DrawScene(Scene scene, Bitmap bitmap);
}