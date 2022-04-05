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

            var intensity = AntiAliasing.GetAntiAliasing(this, scene, Position, Target - Position + pixelOffset, pixelWidth, pixelHeight) ?? scene.BackgroundColor; 
            bitmap.SetPixel(x, y, intensity.AsColor());
        }
    }
}