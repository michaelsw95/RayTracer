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
            if (_expectedSize.HasValue && values.Length != _expectedSize)
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

        public Matrix Create()
        {
            if (!_expectedSize.HasValue)
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
        }

        private readonly List<float[]> _data;
        private int? _expectedSize;
    }
}
