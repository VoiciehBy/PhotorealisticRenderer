using System;

namespace PhotorealisticRenderer
{
    public readonly record struct Vector3(double X, double Y, double Z)
    {
        public static Vector3 operator -(Vector3 vecA) => new(-vecA.X, -vecA.Y, -vecA.Z);

        public static Vector3 operator +(Vector3 vecA, Vector3 vecB) => new(vecA.X + vecB.X, vecA.Y + vecB.Y, vecA.Z + vecB.Z);
        public static Vector3 operator -(Vector3 vecA, Vector3 vecB) => new(vecA.X - vecB.X, vecA.Y - vecB.Y, vecA.Z - vecB.Z);

        public static Vector3 operator +(Vector3 vecA, double val) => new(vecA.X + val, vecA.Y + val, vecA.Z + val);
        public static Vector3 operator +(double val, Vector3 vecA) => vecA + val;

        public static Vector3 operator *(Vector3 vec, double val) => new(vec.X * val, vec.Y * val, vec.Z * val);
        public static Vector3 operator *(double val, Vector3 vec) => vec * val;

        public static Vector3 operator /(Vector3 vec, double val) => new(vec.X / val, vec.Y / val, vec.Z / val);

        public static double Dot(Vector3 vecA, Vector3 vecB) => vecA.Dot(vecB);
        public double Dot(Vector3 vec) => X * vec.X + Y * vec.Y + Z * vec.Z;

        public static Vector3 Cross(Vector3 vecA, Vector3 vecB) => vecA.Cross(vecB);

        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X);
        }

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);
        public double LengthSq => X * X + Y * Y + Z * Z;

        public Vector3 Normalized
        {
            get
            {
                var l = Length;
                if (l <= 0.0001f)
                {
                    return Zero;
                }

                return this / l;
            }
        }

        public Vector3 Reflect(Vector3 other) => Reflect(this, other);

        public static Vector3 Reflect(Vector3 I, Vector3 N)
        {
            return I - 2 * Dot(I, N) * N;
        }

        public static readonly Vector3 Zero = new(0, 0, 0);
    }

    public readonly record struct Vector2(double X, double Y)
    {
        public static Vector2 operator *(Vector2 vec, double val)
        {
            return new Vector2(vec.X * val, vec.Y * val);
        }

        public static Vector2 operator +(Vector2 vecA, Vector2 vecB)
        {
            return new Vector2(vecA.X + vecB.X, vecA.Y + vecB.Y);
        }
    }
}