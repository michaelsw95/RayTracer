using System;
using System.Collections.Generic;
using RayTracer.Model;

namespace RayTracer.Utility
{
    public class MatrixBuilder
    {
        public MatrixBuilder()
            : this(0)
        {
        }

        public MatrixBuilder(int rowCapacity)
        {
            _data = new List<float[]>(rowCapacity);
        }

        public MatrixBuilder WithRow(params float[] values)
        {
            if (_isBuildingIdentityMatrix || _isBuildingTranslationMatrix || _isBuildingScailingMatrix)
            {
                throw new NotSupportedException(
                    "Cannot add rows to this Matrix");
            }
            else if (_expectedSize.HasValue && values.Length != _expectedSize)
            {
                throw new ArgumentException("Rows must all have the same number of values");
            }
            else if (!_expectedSize.HasValue)
            {
                _expectedSize = values.Length;
            }

            _data.Add(values);

            return this;
        }

        public MatrixBuilder AsIdentityMatrix(int size)
        {
            if (_expectedSize.HasValue)
            {
                throw new NotSupportedException(
                    "Identity Matrices cannot be constructed if rows have already been added");
            }

            _isBuildingIdentityMatrix = true;
            _expectedSize = size;

            return this;
        }

        public MatrixBuilder AsTranslationMatrix(int x, int y, int z)
        {
            const int translationMatrixSize = 4;

            if (_expectedSize.HasValue)
            {
                throw new NotSupportedException(
                    "Translation Matrices cannot be constructed if rows have already been added");
            }

            _isBuildingTranslationMatrix = true;
            _expectedSize = translationMatrixSize;
            _transform = (x, y, z);

            return this;
        }

        public MatrixBuilder AsScailingMatrix(int x, int y, int z)
        {
            const int scailingMatrixSize = 4;

            if (_expectedSize.HasValue)
            {
                throw new NotSupportedException(
                    "Scailing Matrices cannot be constructed if rows have already been added");
            }

            _isBuildingScailingMatrix = true;
            _expectedSize = scailingMatrixSize;
            _transform = (x, y, z);

            return this;
        }

        public Matrix Create()
        {
            if (_isBuildingIdentityMatrix)
            {
                return BuildIdentiyMatrix(_expectedSize.Value);
            }
            else if (_isBuildingTranslationMatrix)
            {
                return BuildTranslationMatrix(_transform);
            }
            else if (_isBuildingScailingMatrix)
            {
                return BuildScailingMatrix(_transform);
            }
            else if (!_expectedSize.HasValue)
            {
                return new Matrix(0);
            }
            else if (_data.Count != _expectedSize)
            {
                throw new InvalidOperationException("Cannot create non-square matrix");
            }

            var matrix = new Matrix(_expectedSize.Value);

            for (int i = 0; i < _expectedSize.Value; i++)
            {
                for (int j = 0; j < _expectedSize.Value; j++)
                {
                    matrix.Set(_data[i][j], i, j);
                }
            }

            return matrix;
        }

        public void Reset(int newRowCapacity = 0)
        {
            _data.Clear();
            _data.Capacity = newRowCapacity;

            _expectedSize = null;
            _isBuildingIdentityMatrix = false;
        }

        private Matrix BuildIdentiyMatrix(int size)
        {
            var matrix = new Matrix(size);

            for (var i = 0; i < _expectedSize; i++)
            {
                matrix.Set(1, i, i);
            }

            return matrix;
        }

        private Matrix BuildTranslationMatrix((int x, int y, int z) translation)
        {
            var matrix = BuildIdentiyMatrix(4);

            matrix.Set(translation.x, 0, 3);
            matrix.Set(translation.y, 1, 3);
            matrix.Set(translation.z, 2, 3);

            return matrix;
        }

        private Matrix BuildScailingMatrix((int x, int y, int z) transform)
        {
            var matrix = BuildIdentiyMatrix(4);

            matrix.Set(transform.x, 0, 0);
            matrix.Set(transform.y, 1, 1);
            matrix.Set(transform.z, 2, 2);

            return matrix;
        }

        private readonly List<float[]> _data;
        private int? _expectedSize;
        private bool _isBuildingIdentityMatrix;
        private bool _isBuildingTranslationMatrix;
        private bool _isBuildingScailingMatrix;
        private (int x, int y, int z) _transform;
    }
}
