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
            if (Size != matrixTwo.Size)
            {
                throw new NotSupportedException(
                    "Matrices of different sizes cannot be multiplied together");
            }

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

        public RayTuple Multiply(RayTuple tuple)
        {
            if (Size != 4)
            {
                throw new NotSupportedException(
                    "Only matrices of Size 4 can be multiplied by tuples");
            }

            var tupleValues = new float[4] {
                tuple.X,
                tuple.Y,
                tuple.Z,
                tuple.W
            };

            var newTupleValue = new float[4];
            for (int i = 0; i < tupleValues.Length; i++)
            {
                var row = GetRow(i);

                var product = 0F;
                for (int j = 0; j < Size; j++)
                {
                    product += row[j] * tupleValues[j];
                }

                newTupleValue[i] = product;
            }

            return new RayTuple(
                newTupleValue[0],
                newTupleValue[1],
                newTupleValue[2],
                newTupleValue[3]);
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
