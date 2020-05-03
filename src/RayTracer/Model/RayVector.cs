using System;

namespace RayTracer.Model
{
    public class RayVector : RayTuple
    {
        public RayVector(float x, float y, float z)
            : base(x, y, z, W_IDENTIFIER)
        {
        }

        public double Magnitude()
        {
            if (IsPoint())
            {
                throw new InvalidOperationException(
                    "Magnitude of points cannot be calcualted");
            }

            var sumOfParametersSquared =
                (X * X) + (Y * Y) + (Z * Z) + (W * W);

            return Math.Sqrt(sumOfParametersSquared);
        }

        public RayVector Normalise()
        {
            var magnitude = Magnitude();

            var newX = (float)(X / magnitude);
            var newY = (float)(Y / magnitude);
            var newZ = (float)(Z / magnitude);
            var newW = (float)(W / magnitude);

            if (IsPoint(newW))
            {
                throw new InvalidOperationException("Points cannot be normalised");
            }

            return new RayVector(newX, newY, newZ);
        }

        public float DotProduct(RayTuple other)
        {
            if (IsPoint())
            {
                throw new InvalidOperationException(
                    "Dot product of points cannot be calcualted");
            }

            return (X * other.X) +
                   (Y * other.Y) +
                   (Z * other.Z) +
                   (W * other.Z);
        }

        public RayVector CrossProduct(RayTuple other)
        {
            var newX = (Y * other.Z) - (Z * other.Y);
            var newY = (Z * other.X) - (X * other.Z);
            var newZ = (X * other.Y) - (Y * other.X);

            if (IsPoint())
            {
                throw new InvalidOperationException(
                    "Cross product of points cannot be calcualted");
            }

            return new RayVector(newX, newY, newZ);
        }

        private const int W_IDENTIFIER = 0;
    }
}
