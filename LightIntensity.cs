using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Intensity(int R, int G, int B) { r = R / 255f; g = G / 255f; b = B / 255f; }
        public Intensity(int R, int G) { r = R / 255f; g = G / 255f; b = 0.0; }
        public Intensity(int R) { r = R / 255f; g = b = 0.0; }
        public Intensity() { r = g = b = 0.0; }
        double gRed() { return r; }
        double gGreen() { return g; }
        double gBlue() { return b; }

        public Color AsColor() => Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
    }
}