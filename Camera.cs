using PhotorealisticRenderer;
using PhotorealisticRenderer.Shapes;
using System.Drawing;
using static PhotorealisticRenderer.Scene;

public abstract class Camera
{
    public abstract Ray GetRayTo(Vector2 relativeLocation);

    public Bitmap Raytrace(Scene scene, Size imageSize)
    {

        Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);
        for (int y = 0; y < imageSize.Height; y++)
        {
            for (int x = 0; x < imageSize.Width; x++)
            {
                LightIntensity totalColor = LightIntensity.Black;
                for (int i = 0; i < 1; i++)
                {
                    Vector2 pictureCoordinates = new Vector2(x / (double)imageSize.Width * 2 - 1, y / (double)imageSize.Height * 2 - 1);
                    Ray ray = GetRayTo(pictureCoordinates);
                    totalColor = ShadeRay(scene, ray);
                }
                bmp.SetPixel(x, y, Color.FromArgb((int)(totalColor.R), (int)(totalColor.G), (int)(totalColor.B)));

            }
        }
        return bmp;
    }

    public static LightIntensity ShadeRay(Scene scene, Ray ray)
    {
        Shape hit = scene.TraceRay(ray);
        if (hit == null) { return scene.BackgroundColor; }
        LightIntensity finalColor = hit.Color * 255;
        return finalColor;
    }
}
