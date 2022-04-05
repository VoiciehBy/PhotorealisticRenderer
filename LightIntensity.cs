namespace PhotorealisticRenderer
{
    public class Intensity
    {
        public double r;
        public double g;
        public double b;

        public Intensity(double R, double G, double B) { r = R; g = G; b = B; }
        public Intensity(double R, double G) { r = R; g = G; b = 0.0; }
        public Intensity(double R) { r = R; g = b = 0.0; }
        public Intensity() { r = g = b = 0.0; }
        double gRed() => r;
        double gGreen() => g;
        double gBlue() => b;

        public static Intensity operator +(Intensity i, Intensity ii) => new Intensity(i.r + ii.r, i.g + ii.g, i.b + ii.b);
        public static Intensity operator /(Intensity i, double a) => new Intensity(i.r * a, i.g * a, i.b * a);
        
        public override string ToString() => (this.GetType().Name + ":[R:" + r + ";G:" + g + ";B:" + b + "]");
    }
}