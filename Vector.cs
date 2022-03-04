using System;
namespace PhotorealisticRenderer
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector(double x = 0, double y = 0, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public static void assign(Vector v, Vector v1)
        {
            v.X = v1.X;
            v.Y = v1.Y;
            v.Z = v1.Z;
        }
        public double length() => Math.Sqrt(Utility.pow2(X) + Utility.pow2(Y) + Utility.pow2(Z));
        public double lengthPow2() => Utility.pow2(length());

        public static Vector operator +(Vector v) => v;
        public static Vector operator -(Vector v) => new Vector(-v.X, -v.Y, -v.Z);

        public static Vector operator *(double a, Vector v) => new Vector(a * v.X, a * v.Y, a * v.Z);
        public static Vector operator *(Vector v, double a) => a * v;
        public static Vector operator /(Vector v, double a)
        {
            if (a == 0)
                throw new DivideByZeroException();
            else
                return new Vector(a / v.X, a / v.Y, a / v.Z);
        }
        public static Vector operator /(Vector v, Vector v1)
        {
            if (Utility.isAnyNumberEqualsZero(v) || Utility.isAnyNumberEqualsZero(v1))
                throw new DivideByZeroException();
            else
                return new Vector(v.X / v1.X, v.Y / v1.Y, v.Z / v1.Z);
        }
        public static Vector operator *(Vector v, Vector v1) => new Vector(v.X * v1.X, v.Y * v1.Y, v.Z * v1.Z);
        public static Vector crossProductOf(Vector v, Vector v1)
        {
            double x = (v.Y * v1.Z) - (v1.Y * v.Z);
            double y = (v.X * v1.Z) - (v1.X * v.Z);
            double z = (v.X * v1.Y) - (v1.X * v1.Y);
            return new Vector(x, y, z);
        }
        public static Vector x(Vector v, Vector v1) => crossProductOf(v, v1);
        public static double dotProductOf(Vector v, Vector v1) => (v.X * v1.X) + (v.Y * v1.Y) + (v.Z * v1.Z);
        public static double o(Vector v, Vector v1) => dotProductOf(v,v1);
        public static Vector operator +(Vector v, Vector v1) => new Vector(v.X + v1.X, v.Y + v1.Y, v.Z + v1.Z);
        public static Vector operator -(Vector v, Vector v1) => new Vector(v.X - v1.X, v.Y - v1.Y, v.Z - v1.Z);
        public override string ToString() => ("[" + X + ";" + Y + ";" + Z + "]");
    }
}