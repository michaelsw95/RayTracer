﻿using System;
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
            var newW = (RayTupleType)((int)W + (int)other.W);

            if (newW == RayTupleType.Vector)
            {
                return new RayVector(newX, newY, newZ);
            }
            else if (newW == RayTupleType.Point)
            {
                return new RayPoint(newX, newY, newZ);
            }

            return new RayTuple(newX, newY, newZ, newW);
        }

        public RayTuple Subtract(RayTuple other)
        {
            var newX = X - other.X;
            var newY = Y - other.Y;
            var newZ = Z - other.Z;
            var newW = (RayTupleType)((int)W - (int)other.W);

            if (newW == RayTupleType.Vector)
            {
                return new RayVector(newX, newY, newZ);
            }
            else if (newW == RayTupleType.Point)
            {
                return new RayPoint(newX, newY, newZ);
            }

            return new RayTuple(newX, newY, newZ, newW);
        }

        public RayTuple Negate()
        {
            var newX = X * -1;
            var newY = Y * -1;
            var newZ = Z * -1;
            var newW = (RayTupleType)((int)W * -1);

            if (newW == RayTupleType.Vector)
            {
                return new RayVector(newX, newY, newZ);
            }
            else if (newW == RayTupleType.Point)
            {
                return new RayPoint(newX, newY, newZ);
            }

            return new RayTuple(newX, newY, newZ, newW);
        }
    }
}
