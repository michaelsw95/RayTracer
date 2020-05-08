using AutoFixture;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class MatricTests
    {
        private readonly Fixture _fixture;

        public MatricTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Matrix_CanConstructMatrixWithNumberOfRowsAndColumns()
        {
            // Arrange
            var rows = _fixture.Create<int>();
            var columns = _fixture.Create<int>();

            // Act
            var matrix = new Matrix(rows, columns);

            // Assert
            Assert.Equal(rows, matrix.Rows);
            Assert.Equal(columns, matrix.Columns);
        }

        [Fact]
        public void Matrix_CanGetAndSetValues()
        {
            // Arrange
            var rows = _fixture.Create<int>();
            var columns = _fixture.Create<int>();
            var matrix = new Matrix(rows, columns);

            // Act
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix.Set(i + j, i, j);
                }
            }

            // Assert
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Assert.Equal(i + j, matrix.Get(i, j));
                }
            }
        }

        [Fact]
        public void Matrix_CanBeConstructedUsingTwoDMatrix()
        {
            // Arrange
            var data = new float[10, 10]
            {
                { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10 },
                { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
                { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
                { 41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
                { 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 },
                { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70 },
                { 71, 72, 73, 74, 75, 76, 77, 78, 79, 80 },
                { 81, 82, 83, 84, 85, 86, 87, 88, 89, 90 },
                { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100 },
            };

            // Act
            var matrix = new Matrix(data);

            // Assert
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var dataValue = data[i, j];
                    Assert.Equal(dataValue, matrix.Get(i, j));
                }
            }
        }

        [Fact]
        public void Matrix_IsEqual_ReturnsTrueIfTwoMatricesAreTheSame()
        {
            // Arrange
            var rows = _fixture.Create<int>();
            var columns = _fixture.Create<int>();
            var matrixOne = new Matrix(rows, columns);
            var matrixTwo = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixOne.Set(i + j, i, j);
                    matrixTwo.Set(i + j, i, j);
                }
            }

            // Act
            var isEqual = matrixOne.IsEqual(matrixTwo);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_IsEqual_ReturnsFalseIfTwoMatricesAreTheSame()
        {
            // Arrange
            var rows = _fixture.Create<int>();
            var columns = _fixture.Create<int>();
            var matrixOne = new Matrix(rows, columns);
            var matrixTwo = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixOne.Set(i + j, i, j);
                    matrixTwo.Set(i + j, i, j);
                }
            }

            matrixTwo.Set(matrixTwo.Get(0, 0) + 1, 0, 0);

            // Act
            var isEqual = matrixOne.IsEqual(matrixTwo);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void Matrix_IsEqual_ReturnsTrueIfMatrixPositionValuesAreDifferentByLessThanFiveDP()
        {
            // Arrange
            var rows = _fixture.Create<int>();
            var columns = _fixture.Create<int>();
            var matrixOne = new Matrix(rows, columns);
            var matrixTwo = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixOne.Set(i + j, i, j);
                    matrixTwo.Set(i + j, i, j);
                }
            }

            matrixTwo.Set(matrixTwo.Get(0, 0) + 0.000005F, 0, 0);

            // Act
            var isEqual = matrixOne.IsEqual(matrixTwo);

            // Assert
            Assert.True(isEqual);
        }
    }
}
