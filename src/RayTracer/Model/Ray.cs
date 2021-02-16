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

        public Intersection[] GetIntersects(Sphere sphere, Ray ray)
        {
            var sphereToRay = ray.Origin.Subtract(sphere.CentrePoint) as RayVector;

            var partA = ray.Direction.DotProduct(ray.Direction);
            var partB = 2 * ray.Direction.DotProduct(sphereToRay);
            var partC = sphereToRay.DotProduct(sphereToRay) - 1;

            if (ThereAreNoIntersections(out var discriminant))
            {
                return new Intersection[0];
            }

            var highIntersection = (-partB - Math.Sqrt(discriminant)) / (2 * partA);
            var lowIntersection = (-partB + Math.Sqrt(discriminant)) / (2 * partA);

            return new Intersection[2]
            {
                new Intersection((float)highIntersection, sphere),
                new Intersection((float)lowIntersection, sphere)
            };

            bool ThereAreNoIntersections(out float discriminant)
            {
                discriminant = (float)Math.Pow(partB, 2) - 4 * partA * partC;

                return discriminant < 0;
            }
        }
    }
}
