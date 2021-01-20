using System;
using AutoFixture;
using RayTracer.Model;
using RayTracer.Utility;
using RayTracer.Utility.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class MatrixTests
    {
        private readonly Fixture _fixture;

        public MatrixTests()
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

            var identityMatrix = Transformation.GetIdentiyMatrix(3);

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
            var identityMatrix = Transformation.GetIdentiyMatrix(3);

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

        [Fact]
        public void Matrix_Inverse_ThrowsIfDeterminantIsZero()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(-4, 2, -2, -3)
                .WithRow(9, 6, 2, 6)
                .WithRow(0, -5, 1, 5)
                .WithRow(0, 0, 0, 0)
                .Create();

            // Act / Assert
            Assert.Equal(0, matrix.Determinant());
            Assert.Throws<NotSupportedException>(() => matrix.Inverse());
        }

        [Fact]
        public void Matrix_Inverse_ReturnsTheCorrectInvertedMatrix()
        {
            // Arrange
            var matrix = new MatrixBuilder()
                .WithRow(-5, 2, 6, -8)
                .WithRow(1, -5, 1, 8)
                .WithRow(7, 7, -6, -7)
                .WithRow(1, -3, 7, 4)
                .Create();

            // Act
            var inverseMatrix = matrix.Inverse();

            // Assert
            var expected = new MatrixBuilder()
                .WithRow(0.21805F, 0.45113F, 0.24060F, -0.04511F)
                .WithRow(-0.80827F, -1.45677F, -0.44361F, 0.52068F)
                .WithRow(-0.07895F, -0.22368F, -0.05263F, 0.19737F)
                .WithRow(-0.52256F, -0.81391F, -0.30075F, 0.30639F)
                .Create();

            var isEqual = inverseMatrix.IsEqual(expected);

            Assert.True(isEqual);
            Assert.Equal(532, matrix.Determinant());
            Assert.Equal(-160, matrix.Cofactor(2, 3));
            Assert.Equal(105, matrix.Cofactor(3, 2));
            Assert.Equal(-160F / 532F, inverseMatrix.Get(3, 2));
            Assert.Equal(105F / 532F, inverseMatrix.Get(2, 3));
        }

        [Fact]
        public void Matrix_Inverse_ReturnsTheCorrectValues()
        {
            // Arrange
            var matrixOne = new MatrixBuilder()
                .WithRow(8, -5, 9, 2)
                .WithRow(7, 5, 6, 1)
                .WithRow(-6, 0, 9, 6)
                .WithRow(-3, 0, -9, -4)
                .Create();

            var matrixTwo = new MatrixBuilder()
                .WithRow(9, 3, 0, 9)
                .WithRow(-5, -2, -6, -3)
                .WithRow(-4, 9, 6, 4)
                .WithRow(-7, 6, 6, 2)
                .Create();

            // Act
            var inverseMatrixOne = matrixOne.Inverse();
            var inverseMatrixTwo = matrixTwo.Inverse();

            // Assert
            var expectedOne = new MatrixBuilder()
                .WithRow(-0.15385F, -0.15385F, -0.28205F, -0.53846F)
                .WithRow(-0.07692F, 0.12308F, 0.02564F, 0.03077F)
                .WithRow(0.35897F, 0.35897F, 0.43590F, 0.92308F)
                .WithRow(-0.69231F, -0.69231F, -0.76923F, -1.92308F)
                .Create();

            var expectedTwo = new MatrixBuilder()
                .WithRow(-0.04074F, -0.07778F, 0.14444F, -0.22222F)
                .WithRow(-0.07778F, 0.03333F, 0.36667F, -0.33333F)
                .WithRow(-0.02901F, -0.14630F, -0.10926F, 0.12963F)
                .WithRow(0.17778F, 0.06667F, -0.26667F, 0.33333F)
                .Create();

            var isEqualOne = inverseMatrixOne.IsEqual(expectedOne);
            var isEqualTwo = inverseMatrixTwo.IsEqual(expectedTwo);

            Assert.True(isEqualOne);
            Assert.True(isEqualTwo);
        }

        [Fact]
        public void Matrix_MultiplyProductByInverse_ReturnsOriginalMatrix()
        {
            // Arrange
            var matrixOne = new MatrixBuilder()
                .WithRow(3, -9, 7, 3)
                .WithRow(3, -8, 2, -9)
                .WithRow(-4, 4, 4, 1)
                .WithRow(-6, 5, -1, 1)
                .Create();

            var matrixTwo = new MatrixBuilder()
                .WithRow(8, 2, 2, 2)
                .WithRow(3, -1, 7, 0)
                .WithRow(7, 0, 5, 4)
                .WithRow(6, -2, 0, 5)
                .Create();

            // Act
            var multipliedInverseMatrix = matrixOne
                .Multiply(matrixTwo)
                .Multiply(matrixTwo.Inverse());

            // Assert
            var isEqual = matrixOne.IsEqual(multipliedInverseMatrix);

            Assert.True(isEqual);
        }
    }
}
