using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Model
{
    public class RayTuple
    {
        public RayTuple(float xPosition, float yPosition, float zPosition, RayTupleType w)
        {
            X = xPosition;
            Y = yPosition;
            Z = zPosition;
            W = w;
        }

        public float X { get; }
        public float Y { get; }
        public float Z { get; }
        public RayTupleType W { get; }
    }
}
