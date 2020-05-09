using System;

namespace RayTracer.Model
{
    public class Matrix
    {
        public Matrix(int rows, int columns)
        {
            _matrix = new float[rows, columns];
        }

        public Matrix(float[,] data)
            : this(data.GetLength(0), data.GetLength(1))
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Set(data[i, j], i, j);
                }
            }
        }

        public int Rows { get => _matrix.GetLength(0); }
        public int Columns { get => _matrix.GetLength(1); }

        public float Get(int row, int column)
        {
            return _matrix[row, column];
        }

        public void Set(float value, int row, int column)
        {
            _matrix[row, column] = value;
        }

        public bool IsEqual(Matrix other)
        {
            static bool FloatIsEqual(float a, float b)
            {
                const float EPSILON = 0.00001F;

                return Math.Abs(a - b) < EPSILON;
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (!FloatIsEqual(_matrix[i, j], other._matrix[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public Matrix Multiply(Matrix matrixTwo)
        {
            var matrix = new Matrix(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var row = GetRow(i);
                    var column = matrixTwo.GetColumn(j);

                    var product = 0F;
                    for (int k = 0; k < Rows; k++)
                    {
                        product += row[k] * column[k];
                    }

                    matrix.Set(product, i, j);
                }
            }

            return matrix;
        }

        private float[] GetRow(int index)
        {
            var row = new float[Columns];

            for (int i = 0; i < Columns; i++)
            {
                row[i] = _matrix[index, i];
            }

            return row;
        }

        private float[] GetColumn(int index)
        {
            var column = new float[Rows];

            for (int i = 0; i < Rows; i++)
            {
                column[i] = _matrix[i, index];
            }

            return column;
        }

        private readonly float[,] _matrix;
    }
}
