using System;
namespace PhotorealisticRenderer
{
    public static class Utility
    {
        public static double pow2(double x) => Math.Pow(x, 2);
        public static bool isAnyNumberEqualsZero(Vector v) => ((v.X == 0) || (v.Y == 0) || (v.Z == 0));
        public static bool isUnitVector(Vector v) => v.length() == 1;
        public static bool isEqual(Vector v, Vector v1) => (v.X == v1.X) && (v.Y == v1.Y) && (v.Z == v1.Z);
        public static bool isParallel(Vector v, Vector v1) => isEqual(Vector.x(v, v1), new Vector(0, 0, 0));
    }
}