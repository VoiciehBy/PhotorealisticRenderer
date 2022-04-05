﻿using System.Drawing;

namespace PhotorealisticRenderer;

public class OrthogonalCamera : Camera
{
    public OrthogonalCamera()
    {}
    
    public OrthogonalCamera(Vector position, Vector target) : base(position, target)
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

            var intensity = AntiAliasing.GetAntiAliasing(this, scene, Position + pixelOffset, Target - Position + pixelOffset, pixelWidth, pixelHeight) ?? scene.BackgroundColor; 
            bitmap.SetPixel(x, y, intensity.AsColor());
        }
    }
}