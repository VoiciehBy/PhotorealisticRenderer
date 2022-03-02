using System;
namespace PhotorealisticRenderer
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Point(double x = 0, double y = 0, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override string ToString()
        {
            return ("(" + X + "," + Y + "," + Z + ")");
        }
    }
}