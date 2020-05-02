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

        public bool IsEqual(RayTuple other)
        {
            static bool FloatIsEqual(float a, float b)
            {
                const float EPSILON = 0.00001F;

                return Math.Abs(a - b) < EPSILON;
            }

            return W == other.W &&
                FloatIsEqual(X, other.X) &&
                FloatIsEqual(Y, other.Y) &&
                FloatIsEqual(Z, other.Z);
        }

        public RayTuple Add(RayTuple other)
        {
            var newX = X + other.X;
            var newY = Y + other.Y;
            var newZ = Z + other.Z;
            var newW = (int)W + (int)other.W;

            if (newW == (int)RayTupleType.Vector)
            {
                return new RayVector(newX, newY, newZ);
            }
            else if (newW == (int)RayTupleType.Point)
            {
                return new RayPoint(newX, newY, newZ);
            }

            throw new NotSupportedException("Points should not be added together");
        }
    }
}
