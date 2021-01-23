using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
