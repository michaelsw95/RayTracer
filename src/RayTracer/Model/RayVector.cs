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

            return new RayVector(newX, newY, newZ);
        }

        public float DotProduct(RayTuple other)
        {
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

            return new RayVector(newX, newY, newZ);
        }

        private const float W_IDENTIFIER = 0F;
    }
}
