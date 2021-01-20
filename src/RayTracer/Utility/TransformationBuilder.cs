using System;
using System.Collections.Generic;
using System.Linq;
using RayTracer.Model;
using RayTracer.Utility.Model;

namespace RayTracer.Utility
{
    public class TransformationBuilder
    {
        private readonly List<Matrix> _queuedTransformations;

        public TransformationBuilder() => _queuedTransformations = new List<Matrix>();

        public TransformationBuilder AddTransformation(Matrix transform)
        {
            _queuedTransformations.Add(transform);

            return this;
        }

        public TransformationBuilder AddTranslation(int transformX, int transformY, int transformZ)
        {
            var transformation = Transformation.GetTranslationMatrix(transformX, transformY, transformZ);

            AddTransformation(transformation);

            return this;
        }

        public TransformationBuilder AddScailing(int transformX, int transformY, int transformZ)
        {
            var transformation = Transformation.GetScailingMatrix(transformX, transformY, transformZ);

            AddTransformation(transformation);

            return this;
        }

        public TransformationBuilder AddRotation(RotationAxis rotation, float rotationRadians)
        {
            var transformation = Transformation.GetRotationMatrix(rotation, rotationRadians);

            AddTransformation(transformation);

            return this;
        }

        public TransformationBuilder AddShearing(ShearingTransform transform)
        {
            var transformation = Transformation.GetShearingMatrix(transform);

            AddTransformation(transformation);

            return this;
        }

        public Matrix GetTransformationMatrix()
        {
            if (!_queuedTransformations.Any())
            {
                throw new InvalidOperationException("No transformations have been added to execute");
            }

            var result = _queuedTransformations.Last();

            for (var i = _queuedTransformations.Count - 2; i >= 0; i--)
            {
                result = result.Multiply(_queuedTransformations[i]);
            }

            return result;
        }

        public RayTuple ApplyTransformations(RayTuple tuple) => GetTransformationMatrix().Multiply(tuple);

        public void Reset() => _queuedTransformations.Clear();
    }
}
