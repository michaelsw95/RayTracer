using System;
using RayTracer.Model;
using RayTracer.Utility.Model;

namespace RayTracer.Utility
{
    public static class Transformation
    {
        public static Matrix GetIdentiyMatrix(int size)
        {
            var matrix = new Matrix(size);

            for (var i = 0; i < size; i++)
            {
                matrix.Set(1, i, i);
            }

            return matrix;
        }

        public static Matrix GetTranslationMatrix(int transformX, int transformY, int transformZ)
        {
            var matrix = GetIdentiyMatrix(TransformationMatrixSize);

            matrix.Set(transformX, 0, 3);
            matrix.Set(transformY, 1, 3);
            matrix.Set(transformZ, 2, 3);

            return matrix;
        }

        public static Matrix GetScailingMatrix(int transformX, int transformY, int transformZ)
        {
            var matrix = GetIdentiyMatrix(TransformationMatrixSize);

            matrix.Set(transformX, 0, 0);
            matrix.Set(transformY, 1, 1);
            matrix.Set(transformZ, 2, 2);

            return matrix;
        }

        public static Matrix GetRotationMatrix(RotationAxis rotation, float rotationRadians)
        {
            var matrix = GetIdentiyMatrix(TransformationMatrixSize);

            if (rotation == RotationAxis.X)
            {
                matrix.Set((float)Math.Cos(rotationRadians), 1, 1);
                matrix.Set(-(float)Math.Sin(rotationRadians), 1, 2);
                matrix.Set((float)Math.Sin(rotationRadians), 2, 1);
                matrix.Set((float)Math.Cos(rotationRadians), 2, 2);
            }
            else if (rotation == RotationAxis.Y)
            {
                matrix.Set((float)Math.Cos(rotationRadians), 0, 0);
                matrix.Set((float)Math.Sin(rotationRadians), 0, 2);
                matrix.Set(-(float)Math.Sin(rotationRadians), 2, 0);
                matrix.Set((float)Math.Cos(rotationRadians), 2, 2);
            }
            else if (rotation == RotationAxis.Z)
            {
                matrix.Set((float)Math.Cos(rotationRadians), 0, 0);
                matrix.Set(-(float)Math.Sin(rotationRadians), 0, 1);
                matrix.Set((float)Math.Sin(rotationRadians), 1, 0);
                matrix.Set((float)Math.Cos(rotationRadians), 1, 1);
            }

            return matrix;
        }

        public static Matrix GetShearingMatrix(ShearingTransform transform)
        {
            var matrix = GetIdentiyMatrix(TransformationMatrixSize);

            matrix.Set(transform.XinProportionToY, 0, 1);
            matrix.Set(transform.XinProportionToZ, 0, 2);
            matrix.Set(transform.YinProportionToX, 1, 0);
            matrix.Set(transform.YinProportionToZ, 1, 2);
            matrix.Set(transform.ZinProportionToX, 2, 0);
            matrix.Set(transform.ZinProportionToY, 2, 1);

            return matrix;
        }

        public const int TransformationMatrixSize = 4;
    }
}
