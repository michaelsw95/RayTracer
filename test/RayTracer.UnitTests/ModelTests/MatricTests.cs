using System;
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
        public void Matrix_CanConstructMatrixWithSize()
        {
            // Arrange
            var size = _fixture.Create<int>();

            // Act
            var matrix = new Matrix(size);

            // Assert
            Assert.Equal(size, matrix.Size);
        }

        [Fact]
        public void Matrix_CanGetAndSetValues()
        {
            // Arrange
            var size = _fixture.Create<int>();
            var matrix = new Matrix(size);

            // Act
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix.Set(i + j, i, j);
                }
            }

            // Assert
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.Equal(i + j, matrix.Get(i, j));
                }
            }
        }

        [Fact]
        public void Matrix_IsEqual_ReturnsFalseIfMatricesHaveDifferentSizes()
        {
            // Arrange
            var size = _fixture.Create<int>();
            var matrixOne = new Matrix(size);
            var matrixTwo = new Matrix(size + 1);

            // Act
            var isEqual = matrixOne.IsEqual(matrixTwo);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void Matrix_IsEqual_ReturnsTrueIfTwoMatricesAreTheSame()
        {
            // Arrange
            var size = _fixture.Create<int>();
            var matrixOne = new Matrix(size);
            var matrixTwo = new Matrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
            var size = _fixture.Create<int>();
            var matrixOne = new Matrix(size);
            var matrixTwo = new Matrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
            var size = _fixture.Create<int>();
            var matrixOne = new Matrix(size);
            var matrixTwo = new Matrix(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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

        [Fact]
        public void Matrix_Multiply_ThrowsIfMatricesOfDifferentSizesAreMultiplied()
        {
            // Arrange
            var builder = new MatrixBuilder();

            var matrixOne = builder
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            builder.Reset();

            var matrixTwo = builder
                .WithRow(1, 2)
                .WithRow(4, 5)
                .Create();

            // Act / Assert
            var multipliedMatrix = Assert.Throws<NotSupportedException>(
                () => matrixOne.Multiply(matrixTwo)
            );
        }

        [Fact]
        public void Matrix_Multiply_ReturnsNewMatrixWithComponentsMultiplied()
        {
            // Arrange
            var matrixOne = new MatrixBuilder(4)
                .WithRow(1, 2, 3, 4)
                .WithRow(5, 6, 7, 8)
                .WithRow(9, 8, 7, 6)
                .WithRow(5, 4, 3, 2)
                .Create();

            var matrixTwo = new MatrixBuilder(4)
                .WithRow(-2, 1, 2, 3)
                .WithRow(3, 2, 1, -1)
                .WithRow(4, 3, 6, 5)
                .WithRow(1, 2, 7, 8)
                .Create();

            // Act
            var multipliedMatrix = matrixOne.Multiply(matrixTwo);

            // Assert
            var expected = new MatrixBuilder(4)
                .WithRow(20, 22, 50, 48)
                .WithRow(44, 54, 114, 108)
                .WithRow(40, 58, 110, 102)
                .WithRow(16, 26, 46, 42)
                .Create();

            var isEqual = multipliedMatrix.IsEqual(expected);
            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_Multiply_ThrowsIfMatrixSizeIsNotFourAndMultiplyingByTuple()
        {
            // Arrange
            var matrix = new MatrixBuilder(4)
                .WithRow(1, 2, 3)
                .WithRow(5, 6, 7)
                .WithRow(9, 10, 11)
                .Create();

            var tuple = new RayTuple(1, 2, 3, 0);

            // Act / Assert
            Assert.Throws<NotSupportedException>(() => matrix.Multiply(tuple));
        }

        [Fact]
        public void Matrix_Multiply_CanTakeATupleAndReturnNewMultipliedByMatrixComponenents()
        {
            // Arrange
            var matrix = new MatrixBuilder(4)
                .WithRow(1, 2, 3, 4)
                .WithRow(2, 4, 4, 2)
                .WithRow(8, 6, 4, 1)
                .WithRow(0, 0, 0, 1)
                .Create();

            var tuple = new RayTuple(1, 2, 3, 1);

            // Act
            var multipliedTuple = matrix.Multiply(tuple);

            // Assert
            var expected = new RayTuple(18, 24, 33, 1);
            var isEqual = expected.IsEqual(multipliedTuple);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_Multiply_ReturnsTheSameMatrixIfMultipliedByIdentityMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            var matrix = builder
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            builder.Reset();

            var identityMatrix = builder
                .AsIdentityMatrix(3)
                .Create();

            // Act
            var multipliedMatrix = matrix
                .Multiply(identityMatrix);

            // Assert
            var isEqual = multipliedMatrix
                .IsEqual(matrix);

            Assert.True(isEqual);
        }
    }
}
