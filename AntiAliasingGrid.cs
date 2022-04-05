using System;

namespace PhotorealisticRenderer;

public class AntiAliasingGrid : AntiAliasing
{
    private int gridSize;

    public int GridSize
    {
        get => gridSize;
        set => gridSize = Math.Max(value, 1);
    }

    public AntiAliasingGrid(int gridSize) => GridSize = gridSize;

    public override Intensity GetAntiAliasing(in Camera camera, in Scene scene, in Vector position, in Vector direction, in float pixelWidth, in float pixelHeight)
    {
        var targetGridSize = GridSize;
        var gridSizeSquared = targetGridSize * targetGridSize;
        var rectWidth = pixelWidth / targetGridSize;
        var rectHeight = pixelHeight / targetGridSize;
        var gridStartX = -(pixelWidth / 2) + (rectWidth / 2);
        var gridStartY = -(pixelHeight / 2) + (rectHeight / 2);
        var r = 0d;
        var g = 0d;
        var b = 0d;

        for (var x = 0; x < targetGridSize; x++)
        for (var y = 0; y < targetGridSize; y++)
        {
            var offsetX = gridStartX + (x * rectWidth);
            var offsetY = gridStartY + (y * rectHeight);
            var offset = direction + (camera.Right * offsetX) + (camera.Up * offsetY);

            var intensity = scene.GetClosestIntersection(new Ray(position, offset), camera.NearPlane, camera.FarPlane) ?? scene.BackgroundColor;

            r += intensity.r;
            g += intensity.g;
            b += intensity.b;
        }

        return new Intensity(r / gridSizeSquared, g / gridSizeSquared, b / gridSizeSquared);
    }
}