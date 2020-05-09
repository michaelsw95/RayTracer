using System;
using System.Collections.Generic;

namespace RayTracer.Model
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
            if (_expectedWidth.HasValue && values.Length != _expectedWidth)
            {
                throw new ArgumentException("Rows must all have the same number of values");
            }
            else if (!_expectedWidth.HasValue)
            {
                _expectedWidth = values.Length;
            }

            _data.Add(values);

            return this;
        }


        public Matrix Create()
        {
            if (!_expectedWidth.HasValue)
            {
                return new Matrix(0, 0);
            }

            var matrix = new Matrix(_data.Count, _expectedWidth.Value);

            for (int i = 0; i < _data.Count; i++)
            {
                for (int j = 0; j < _expectedWidth.Value; j++)
                {
                    matrix.Set(_data[i][j], i, j);
                }
            }

            return matrix;
        }

        private readonly List<float[]> _data;
        private int? _expectedWidth;
    }
}
