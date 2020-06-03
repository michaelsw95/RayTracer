using System;
using AutoFixture;
using RayTracer.Model;
using RayTracer.Utility;
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
            var matrixOne = new Matrix(3);
            var matrixTwo = new Matrix(5);

            var tuple = new RayTuple(1, 2, 3, 0);

            // Act / Assert
            Assert.Throws<NotSupportedException>(() => matrixOne.Multiply(tuple));
            Assert.Throws<NotSupportedException>(() => matrixTwo.Multiply(tuple));
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

        [Fact]
        public void Matrix_Transpose_ReturnsTransposedMatrix()
        {
            // Arrange
            var matrix = new MatrixBuilder(3)
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            // Act
            var transposedMatrix = matrix.Transpose();

            // Assert
            var expected = new MatrixBuilder(3)
                .WithRow(1, 4, 7)
                .WithRow(2, 5, 8)
                .WithRow(3, 6, 9)
                .Create();

            var isEqual = expected.IsEqual(transposedMatrix);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_Transpose_ReturnsTheOriginalMatrixIfCalledTwice()
        {
            // Arrange
            var matrix = new MatrixBuilder(3)
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            // Act
            var transposedMatrix = matrix
                .Transpose()
                .Transpose();

            // Assert
            var isEqual = matrix.IsEqual(transposedMatrix);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_Transpose_TransposedIdentityMatrixIsStillAnIdentityMatrix()
        {
            // Arrange
            var identityMatrix = new MatrixBuilder()
                .AsIdentityMatrix(3)
                .Create();

            // Act
            var transposedMatrix = identityMatrix
                .Transpose();

            // Assert
            var isEqual = identityMatrix
                .IsEqual(transposedMatrix);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_Determinant_ReturnsCorrectValueForA2x2Matrix()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(1, 5)
                .WithRow(-3, 2)
                .Create();

            // Act
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(17, determinant);
        }

        [Fact]
        public void Matrix_Determinant_ReturnsCorrectValueForA3x3Matrix()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(1, 2, 6)
                .WithRow(-5, 8, -4)
                .WithRow(2, 6, 4)
                .Create();

            // Act
            var cofactorOne = matrix.Cofactor(0, 0);
            var cofactorTwo = matrix.Cofactor(0, 1);
            var cofactorThree = matrix.Cofactor(0, 2);
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(56, cofactorOne);
            Assert.Equal(12, cofactorTwo);
            Assert.Equal(-46, cofactorThree);
            Assert.Equal(-196, determinant);
        }

        [Fact]
        public void Matrix_Determinant_ReturnsCorrectValueForA4x4Matrix()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(-2, -8, 3, 5)
                .WithRow(-3, 1, 7, 3)
                .WithRow(1, 2, -9, 6)
                .WithRow(-6, 7, 7, -9)
                .Create();

            // Act
            var cofactorOne = matrix.Cofactor(0, 0);
            var cofactorTwo = matrix.Cofactor(0, 1);
            var cofactorThree = matrix.Cofactor(0, 2);
            var cofactorFour = matrix.Cofactor(0, 3);
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(690, cofactorOne);
            Assert.Equal(447, cofactorTwo);
            Assert.Equal(210, cofactorThree);
            Assert.Equal(51, cofactorFour);
            Assert.Equal(-4071, determinant);
        }

        [Fact]
        public void Matrix_Determinant_ThrowsIfMatrixIsNot2x2Or3x3or4x4()
        {
            // Arrange
            var matrixOne = new Matrix(1);
            var matrixTwo = new Matrix(5);

            // Act / Assert
            Assert.Throws<NotSupportedException>(() => matrixOne.Determinant());
            Assert.Throws<NotSupportedException>(() => matrixTwo.Determinant());
        }

        [Fact]
        public void Matrix_SubMatrix_ReturnsSubMatrixOfGivenMatrix3x3()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            // Act
            var subMatrix = matrix.SubMatrix(0, 2);

            // Assert
            var expected = new MatrixBuilder()
                .WithRow(4, 5)
                .WithRow(7, 8)
                .Create();

            var isEqual = subMatrix.IsEqual(expected);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_SubMatrix_ReturnsSubMatrixOfGivenMatrix4x4()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(1, 2, 3, 4)
                .WithRow(5, 6, 7, 8)
                .WithRow(9, 10, 11, 12)
                .WithRow(13, 14, 15, 16)
                .Create();

            // Act
            var subMatrix = matrix.SubMatrix(2, 1);

            // Assert
            var expected = new MatrixBuilder()
                .WithRow(1, 3, 4)
                .WithRow(5, 7, 8)
                .WithRow(13, 15, 16)
                .Create();

            var isEqual = subMatrix.IsEqual(expected);

            Assert.True(isEqual);
        }

        [Fact]
        public void Matrix_SubMatrix_ThrowsIfRowOrColumnToDeleteIsOutOfRange()
        {
            // Arrange
            var matrix = new Matrix(3);

            // Act / Assert
            Assert.Throws<IndexOutOfRangeException>(() => matrix.SubMatrix(1, 3));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.SubMatrix(3, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.SubMatrix(-1, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.SubMatrix(1, -1));
        }

        [Fact]
        public void Matrix_Minor_ReturnsCorrectValueForA3x3Matrix()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(3, 5, 0)
                .WithRow(2, -1, -7)
                .WithRow(6, -1, 5)
                .Create();

            // Act
            var determinant = matrix.Minor(1, 0);

            // Assert
            Assert.Equal(25, determinant);
        }

        [Fact]
        public void Matrix_Minor_ThrowsIfRowOrColumnToDeleteIsOutOfRange()
        {
            // Arrange
            var matrix = new Matrix(3);

            // Act / Assert
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Minor(1, 3));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Minor(3, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Minor(-1, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Minor(1, -1));
        }

        [Fact]
        public void Matrix_Cofactor_ReturnsCorrectValue()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(3, 5, 0)
                .WithRow(2, -1, -7)
                .WithRow(6, -1, 5)
                .Create();

            // Act
            var minorOne = matrix.Minor(0, 0);
            var cofactorOne = matrix.Cofactor(0, 0);
            var minorTwo = matrix.Minor(1, 0);
            var cofactorTwo = matrix.Cofactor(1, 0);

            // Assert
            Assert.Equal(-12, minorOne);
            Assert.Equal(-12, cofactorOne);
            Assert.Equal(25, minorTwo);
            Assert.Equal(-25, cofactorTwo);
        }

        [Fact]
        public void Matrix_Cofactor_ThrowsIfRowOrColumnToDeleteIsOutOfRange()
        {
            // Arrange
            var matrix = new Matrix(3);

            // Act / Assert
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Cofactor(1, 3));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Cofactor(3, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Cofactor(-1, 1));
            Assert.Throws<IndexOutOfRangeException>(() => matrix.Cofactor(1, -1));
        }
    }
}
