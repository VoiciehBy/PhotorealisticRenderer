using System;

namespace PhotorealisticRenderer;

public class AntiAliasingRandom : AntiAliasing
{
    private int samples;

    public int Samples
    {
        get => samples;
        set => samples = Math.Max(value, 1);
    }

    public AntiAliasingRandom(int samples) => Samples = samples;

    public override Intensity GetAntiAliasing(in Camera camera, in Scene scene, in Vector position, in Vector direction, in float pixelWidth, in float pixelHeight)
    {
        var samplesToProcess = Samples;
        var r = 0d;
        var g = 0d;
        var b = 0d;
        var rand = new Random();
        
        for (var i = 0; i < samplesToProcess; i++)
        {
            var offsetX = (rand.NextDouble() * pixelWidth) - (pixelWidth / 2);
            var offsetY = (rand.NextDouble() * pixelHeight) - (pixelHeight / 2);
            var offset = direction + (camera.Right * offsetX) + (camera.Up * offsetY);
            
            var intensity = scene.GetClosestIntersection(new Ray(position, offset), camera.NearPlane, camera.FarPlane) ?? scene.BackgroundColor;

            r += intensity.r;
            g += intensity.g;
            b += intensity.b;
        }

        return new Intensity(r / samplesToProcess, g / samplesToProcess, b / samplesToProcess);
    }
}