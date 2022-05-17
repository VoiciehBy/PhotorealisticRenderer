using PhotorealisticRenderer;
using System.Drawing;
using System.Threading.Tasks;

public abstract class Camera
{
    public AntiAliasing antiAliasing = new();

    public abstract Ray GetRayTo(Vector2 relativeLocation);

    public Bitmap Raytrace(Scene scene, Size imageSize)
    {
        var halfPixelWidth = 1 / (double)imageSize.Width;
        var halfPixelHeight = 1 / (double)imageSize.Height;

        var bmp = new Bitmap(imageSize.Width, imageSize.Height);
        Parallel.For(0, imageSize.Height, (y, _) =>
        {
            Parallel.For(0, imageSize.Width, (x, _) =>
            {
                //Vector2 pictureCoordinates = new Vector2(x / (double)imageSize.Width * 2 - 1, y / (double)imageSize.Height * 2 - 1);
                //var totalColor = ShadeRay(scene, GetRayTo(pictureCoordinates));
                var totalColor = antiAliasing.GetResult(this, scene, x / (double)imageSize.Width * 2 - 1, y / (double)imageSize.Height * 2 - 1, halfPixelWidth, halfPixelHeight);
                lock (bmp)
                {
                    bmp.SetPixel(x, y, totalColor);
                }
            });
        });
        return bmp;
    }

    public LightIntensity ShadeRay(Scene world, Ray ray)
    {
        RayHit info = world.TraceRay(ray);
        if (info.Hit == null) { return world.BackgroundColor; }
        LightIntensity finalColor = LightIntensity.Black;
        PhongMaterialBase material = info.Hit.Material;
        foreach (var light in world.Lights)
        {
            // if (world.CheckBetween(info.HitPoint, light.Position)) { continue; }
            finalColor += material.Radiance(light, info);
        }
        return finalColor;
    }
}