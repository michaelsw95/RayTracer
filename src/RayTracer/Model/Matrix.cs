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

        private readonly float[,] _matrix;
    }
}
