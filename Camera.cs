using PhotorealisticRenderer;
using PhotorealisticRenderer.Shapes;
using System.Drawing;
using static PhotorealisticRenderer.Scene;

public abstract class Camera
{
    public AntiAliasing antiAliasing = new();
    
    public abstract Ray GetRayTo(Vector2 relativeLocation);

    public Bitmap Raytrace(Scene scene, Size imageSize)
    {
        var halfPixelWidth = 1 / (double)imageSize.Width;
        var halfPixelHeight = 1 / (double)imageSize.Height;

        Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);
        for (int y = 0; y < imageSize.Height; y++)
        {
            for (int x = 0; x < imageSize.Width; x++)
            {
                LightIntensity totalColor = antiAliasing.GetResult(this, scene, x / (double)imageSize.Width * 2 - 1, y / (double)imageSize.Height * 2 - 1, halfPixelWidth, halfPixelHeight);
                bmp.SetPixel(x, y, totalColor);

            }
        }
        return bmp;
    }

    public static LightIntensity ShadeRay(Scene scene, Ray ray)
    {
        Shape hit = scene.TraceRay(ray);
        if (hit == null) { return scene.BackgroundColor; }
        return hit.Color;
    }
}
