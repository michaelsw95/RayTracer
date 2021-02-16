using System.Linq;

namespace RayTracer.Model
{
    public class Intersection
    {
        public float Value { get; init; }
        public WorldObject Object { get; init; }

        public Intersection(float value, WorldObject intersectionObject)
        {
            Value = value;
            Object = intersectionObject;
        }

        public static Intersection[] Aggregate(params Intersection[] intersections) => intersections;
        
        public static Intersection GetHit(Intersection[] intersections) =>
            intersections
                .OrderBy(o => o.Value)
                .Where(o => o.Value > 0)
                .FirstOrDefault();
    }
}
