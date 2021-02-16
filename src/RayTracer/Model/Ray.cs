using System;

namespace RayTracer.Model
{
    public class Ray
    {
        public Ray(RayPoint origin, RayVector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public RayPoint Origin { get; }
        public RayVector Direction { get; }

        public RayPoint GetPosition(float distance) => Direction.Multiply(distance).Add(Origin) as RayPoint;

        public float[] GetIntersects(Sphere sphere, Ray ray)
        {
            var sphereToRay = ray.Origin.Subtract(sphere.CentrePoint) as RayVector;

            var partA = ray.Direction.DotProduct(ray.Direction);
            var partB = 2 * ray.Direction.DotProduct(sphereToRay);
            var partC = sphereToRay.DotProduct(sphereToRay) - 1;

            var discriminant = Math.Pow(partB, 2) - 4 * partA * partC;

            if (discriminant < 0)
            {
                return new float[0];
            }

            return new float[2]
            {
                (float)(-partB - Math.Sqrt(discriminant)) / (2 * partA),
                (float)(-partB + Math.Sqrt(discriminant)) / (2 * partA)
            };
        }
    }
}
