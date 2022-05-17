using System;
using System.Drawing;

namespace PhotorealisticRenderer
{
    public readonly record struct LightIntensity(double R, double G, double B)
    {
        public LightIntensity(double color) : this(color, color, color)
        { }

        public double MaxDiffBetween(LightIntensity other)
        {
            var diffR = Math.Abs(R - other.R);
            var diffG = Math.Abs(G - other.G);
            var diffB = Math.Abs(B - other.B);

            return Math.Max(diffR, Math.Max(diffG, diffB));
        }
        
        public static implicit operator LightIntensity(Color color) => new(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        public static implicit operator Color(LightIntensity intensity) => Color.FromArgb((int)Math.Min(Math.Max(intensity.R * 255, 0), 255), (int)Math.Min(Math.Max(intensity.G * 255, 0), 255), (int)Math.Min(Math.Max(intensity.B * 255, 0), 255));
        public static LightIntensity operator +(LightIntensity intensity, LightIntensity col2) => new(intensity.R + col2.R, intensity.G + col2.G, intensity.B + col2.B);
        public static LightIntensity operator +(LightIntensity intensity, double val) => new(intensity.R + val, intensity.G + val, intensity.B + val);
        public static LightIntensity operator -(LightIntensity intensity, LightIntensity col2) => new(intensity.R - col2.R, intensity.G - col2.G, intensity.B - col2.B);
        public static LightIntensity operator -(LightIntensity intensity, double val) => new(intensity.R - val, intensity.G - val, intensity.B - val);
        public static LightIntensity operator *(LightIntensity intensity, double val) => new(intensity.R * val, intensity.G * val, intensity.B * val);
        public static LightIntensity operator *(LightIntensity intensity, LightIntensity intensity2) => new(intensity.R * intensity2.R, intensity.G * intensity2.G, intensity.B * intensity2.B);
        public static LightIntensity operator /(LightIntensity intensity, double val) => intensity * (1 / val);

        public static readonly LightIntensity White = new(1, 1, 1);
        public static readonly LightIntensity Black = new(0, 0, 0);

        public override string ToString() => $"R: {R:F2}, G: {G:F2}, B: {B:F2}";
    }
}