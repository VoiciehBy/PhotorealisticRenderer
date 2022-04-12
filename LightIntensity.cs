using System.Drawing;

namespace PhotorealisticRenderer
{
    public class LightIntensity
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
        public LightIntensity(double R, double G, double B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }
        public static implicit operator LightIntensity(Color color)
        { return new LightIntensity(color.R / 255.0, color.G / 255.0, color.B / 255.0); }
        public static LightIntensity operator +(LightIntensity intensity, LightIntensity col2)
        { return new LightIntensity(intensity.R + col2.R, intensity.G + col2.G, intensity.B + col2.B); }
        public static LightIntensity operator +(LightIntensity intensity, double val)
        { return new LightIntensity(intensity.R + val, intensity.G + val, intensity.B + val); }
        public static LightIntensity operator -(LightIntensity intensity, double val)
        { return new LightIntensity(intensity.R - val, intensity.G - val, intensity.B - val); }
        public static LightIntensity operator *(LightIntensity intensity, double val)
        { return new LightIntensity(intensity.R * val, intensity.G * val, intensity.B * val); }
        public static LightIntensity operator *(LightIntensity intensity, LightIntensity intensity2)
        { return new LightIntensity(intensity.R * intensity2.R, intensity.G * intensity2.G, intensity.B * intensity2.B); }
        public static LightIntensity operator /(LightIntensity intensity, double val)
        { return intensity * (1 / val); }
        public static readonly LightIntensity White = new(1, 1, 1);
        public static readonly LightIntensity Black = new(0, 0, 0);
    }
}