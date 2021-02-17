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

        public Intersection[] GetIntersects(Sphere sphere)
        {
            var invertedObjectTransform = sphere.Transform.Inverse();
            var mutatedRay = new Ray(Origin, Direction).Transform(invertedObjectTransform);

            var sphereToRay = mutatedRay.Origin.Subtract(sphere.CentrePoint) as RayVector;

            var partA = mutatedRay.Direction.DotProduct(mutatedRay.Direction);
            var partB = 2 * mutatedRay.Direction.DotProduct(sphereToRay);
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

        public Ray Transform(Matrix transformation)
        {
            var transformedOrigin = Origin.Multiply(transformation) as RayPoint;
            var transformedDirection = Direction.Multiply(transformation) as RayVector;

            var transformedRay = new Ray(transformedOrigin, transformedDirection);

            return transformedRay;
        }
    }
}
