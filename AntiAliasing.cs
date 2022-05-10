using System;
using System.Xml.Schema;

namespace PhotorealisticRenderer;

public class AntiAliasing
{
    public int MaxSamples { get; } = 5;
    public double Tolerance { get; } = 0.05;

    public LightIntensity GetResult(in Camera camera, in Scene scene, in double x, in double y, in double halfPixelWidth, in double halfPixelHeight)
    {
        int total = 0;
        PixelRect center = new PixelRect(camera, scene, x, y, halfPixelWidth, halfPixelHeight, ref total, MaxSamples, Tolerance);
        return center.GetIntensity(null);
    }

    private class PixelPoint
    {
        public Vector2 CenterPoint { get; }
        public LightIntensity Center { get; }

        public PixelPoint(in Camera camera, in Scene scene, in double x, in double y)
        {
            CenterPoint = new Vector2(x, y);
            Ray ray = camera.GetRayTo(new Vector2(x, y));
            Center = Camera.ShadeRay(scene, ray);
        }

        public virtual LightIntensity GetIntensity(LightIntensity parent)
        {
            return (Center + parent) / 2;
        }
    }

    private class PixelRect : PixelPoint
    {
        protected PixelPoint TopRight;
        protected PixelPoint BottomRight;
        protected PixelPoint BottomLeft;
        protected PixelPoint TopLeft;

        public PixelRect(in Camera camera, in Scene scene, in double x, in double y, in double halfWidth, in double halfHeight, ref int total, in int max, in double tolerance)
            : base(camera, scene, x, y)
        {
            PixelPoint topRight = new PixelPoint(camera, scene, x + halfWidth, y + halfHeight);
            PixelPoint bottomRight = new PixelPoint(camera, scene, x + halfWidth, y - halfHeight);
            PixelPoint bottomLeft = new PixelPoint(camera, scene, x - halfWidth, y - halfHeight);
            PixelPoint topLeft = new PixelPoint(camera, scene, x - halfWidth, y + halfHeight);

            total++;
            if (total >= max)
            {
                TopRight = topRight;
                BottomRight = bottomRight;
                BottomLeft = bottomLeft;
                TopLeft = topLeft;
                return;
            }

            PixelPoint top = null;
            PixelPoint right = null;
            PixelPoint bottom = null;
            PixelPoint left = null;

            double innerHalfWidth = halfWidth / 2;
            double innerHalfHeight = halfHeight / 2;

            if (Center.MaxDiffBetween(topRight.Center) <= tolerance)
            {
                TopRight = topRight;
            }
            else
            {
                TopRight = new PixelRect(camera, scene, topRight.CenterPoint.X, topRight.CenterPoint.Y, innerHalfWidth, innerHalfHeight, ref total, max, tolerance);

                if (total >= max)
                {
                    BottomRight = bottomRight;
                    BottomLeft = bottomLeft;
                    TopLeft = topLeft;
                    return;
                }
            }

            if (Center.MaxDiffBetween(bottomRight.Center) <= tolerance)
            {
                BottomRight = bottomRight;
            }
            else
            {
                BottomRight = new PixelRect(camera, scene, bottomRight.CenterPoint.X, bottomRight.CenterPoint.Y, innerHalfWidth, innerHalfHeight, ref total, max, tolerance);

                if (total >= max)
                {
                    BottomLeft = bottomLeft;
                    TopLeft = topLeft;
                    return;
                }
            }

            if (Center.MaxDiffBetween(bottomLeft.Center) <= tolerance)
            {
                BottomLeft = bottomLeft;
            }
            else
            {
                BottomLeft = new PixelRect(camera, scene, bottomLeft.CenterPoint.X, bottomLeft.CenterPoint.Y, innerHalfWidth, innerHalfHeight, ref total, max, tolerance);

                if (total >= max)
                {
                    TopLeft = topLeft;
                    return;
                }
            }

            if (Center.MaxDiffBetween(topLeft.Center) <= tolerance)
            {
                TopLeft = topLeft;
            }
            else
            {
                TopLeft = new PixelRect(camera, scene, topLeft.CenterPoint.X, topLeft.CenterPoint.Y, innerHalfWidth, innerHalfHeight, ref total, max, tolerance);
            }
        }

        public override LightIntensity GetIntensity(LightIntensity parent)
        {
            return
                (
                    TopRight.GetIntensity(Center) +
                    BottomRight.GetIntensity(Center) +
                    BottomLeft.GetIntensity(Center) +
                    TopLeft.GetIntensity(Center)
                ) / 4;
        }
    }
}