using System;
namespace PhotorealisticRenderer
{
    public class Vector
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Vector(Point start, Point end)
        {
            Start = start;
            End = end;
        }
        public double distance() => Math.Sqrt(Utility.pow2(End.X - Start.X) + Utility.pow2(End.Y - Start.Y) + Utility.pow2(End.Z - Start.Z));
        public override string ToString()
        {
            return " = [" + Start.ToString() + End.ToString() + "]\n";
        }
    }
}