using System;

namespace RayTracer.Model
{
    public class Matrix
    {
        public Matrix(int size)
        {
            _matrix = new float[size, size];
        }

        public int Size { get => _matrix.GetLength(0); }

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

            if (Size != other.Size)
            {
                return false;
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
            var matrix = new Matrix(Size);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var row = GetRow(i);
                    var column = matrixTwo.GetColumn(j);

                    var product = 0F;
                    for (int k = 0; k < Size; k++)
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
            var row = new float[Size];

            for (int i = 0; i < Size; i++)
            {
                row[i] = _matrix[index, i];
            }

            return row;
        }

        private float[] GetColumn(int index)
        {
            var column = new float[Size];

            for (int i = 0; i < Size; i++)
            {
                column[i] = _matrix[i, index];
            }

            return column;
        }

        private readonly float[,] _matrix;
    }
}
