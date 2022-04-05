using System.Collections.Generic;

namespace PhotorealisticRenderer;

public abstract class AntiAliasing
{
    public abstract Intensity GetAntiAliasing(in Camera camera, in Scene scene, in Vector position, in Vector direction, in float pixelWidth, in float pixelHeight);

    private static AntiAliasing noAntiAliasingInstance;
    public static AntiAliasing None => noAntiAliasingInstance ??= new AntiAliasingNone();
    
    private class AntiAliasingNone : AntiAliasing
    {
        public override Intensity GetAntiAliasing(in Camera camera, in Scene scene, in Vector position, in Vector direction, in float pixelWidth, in float pixelHeight)
            => scene.GetClosestIntersection(new Ray(position, direction), camera.NearPlane, camera.FarPlane);
    }
}