using System;

namespace RayTracer.Model
{
    public class Sphere
    {
        private Guid _id;

        public RayPoint CentrePoint { get; init; }
        public float Radii;


        public Sphere()
        {
            _id = Guid.NewGuid();

            // TODO - Assign dynamically
            CentrePoint = new RayPoint(0, 0, 0);
            Radii = 1;
        }
    }
}