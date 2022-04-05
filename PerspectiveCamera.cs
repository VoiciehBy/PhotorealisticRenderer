using System.Diagnostics;
using System.Drawing;

namespace PhotorealisticRenderer;

public class PerspectiveCamera : Camera
{
    public PerspectiveCamera()
    {}
    
    public PerspectiveCamera(Vector position, Vector target) : base(position, target)
    {}
    
    public override void DrawScene(Scene scene, Bitmap bitmap)
    {
        if (bitmap == null)
            return;
        
        var pixelWidth = 2.0f / bitmap.Width;
        var pixelHeight = 2.0f / bitmap.Height;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixelOffset = (Right * (-1.0f + (x + 0.5f) * pixelWidth)) + (Up * (1.0f - (y + 0.5f) * pixelHeight));
            // var pixelOffset = new Vector(-1.0f + (x + 0.5f) * pixelWidth, 1.0f - (y + 0.5f) * pixelHeight);
            var ray = new Ray(Position, Target - Position + pixelOffset);

            var intensity = scene.GetClosestIntersection(ray, NearPlane, FarPlane);

            if (intensity == null) bitmap.SetPixel(x, y, scene.BackgroundColor.AsColor());
            else bitmap.SetPixel(x, y, intensity.AsColor());
        }
    }
}