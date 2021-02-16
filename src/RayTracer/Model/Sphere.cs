namespace RayTracer.Model
{
    public class Sphere : WorldObject
    {
        public RayPoint CentrePoint { get; init; }
        public float Radii { get; init; }

        public Sphere()
        {
            // TODO - Assign dynamically
            CentrePoint = new RayPoint(0, 0, 0);
            Radii = 1;
        }
    }
}
