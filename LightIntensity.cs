using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotorealisticRenderer
{
    class Intensity
    {
        public double r;
        public double g;
        public double b;

        Intensity(double R, double G, double B) { r = R; g = G; b = B; }
        Intensity(double R, double G) { r = R; g = G; b = 0.0; }
        Intensity(double R) { r = R; g = b = 0.0; }
        Intensity() { r = g = b = 0.0; }
        double gRed() { return r; }
        double gGreen() { return g; }
        double gBlue() { return b; }
    }
}