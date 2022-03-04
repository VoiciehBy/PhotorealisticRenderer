using System;
namespace PhotorealisticRenderer
{
    public static class Utility
    {
        public static double pow2(double x) => Math.Pow(x, 2);
        public static bool isAnyNumberEqualsZero(Vector v) => ( (v.X == 0) || (v.Y == 0) || (v.Z == 0) );
        public static bool isUnitVector(Vector v) => v.length() == 1;
    }
}