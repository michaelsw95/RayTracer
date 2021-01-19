using System;
using RayTracer.Model;
using RayTracer.Utility;
using Xunit;

namespace RayTracer.UnitTests.UtilityTests
{
    public class MatrixBuilderTests
    {
        [Fact]
        public void MatrixBuilder_CanConstructEmptyMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            var matrix = builder
                .Create();

            // Assert
            Assert.Equal(0, matrix.Size);
        }

        [Fact]
        public void MatrixBuilder_CanConstructMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            var matrix = builder
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9)
                .Create();

            // Assert
            Assert.Equal(3, matrix.Size);
        }

        [Fact]
        public void MatrixBuilder_WithRow_ThrowsIfInconsistentColumnsAreUsed()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6)
                .WithRow(7, 8, 9);

            // Assert
            Assert.Throws<ArgumentException>(() => builder.WithRow(10, 11));
            Assert.Throws<ArgumentException>(() => builder.WithRow(10, 11, 12, 13));
        }

        [Fact]
        public void MatrixBuilder_WithRow_ThrowsIfBuildingAnIdentityMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.AsIdentityMatrix(4);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.WithRow(10, 11));
        }

        [Fact]
        public void MatrixBuilder_WithRow_ThrowsIfBuildingAnTranslationMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.AsTranslationMatrix(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.WithRow(10, 11));
        }

        [Fact]
        public void MatrixBuilder_WithRow_ThrowsIfBuildingAnScailingMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.AsScailingMatrix(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.WithRow(10, 11));
        }

        [Fact]
        public void MatrixBuilder_WithRow_ThrowsIfBuildingAnRotationMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.AsRotationMatrix(RotationAxis.X, 0);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.WithRow(10, 11));
        }

        [Fact]
        public void MatrixBuilder_Create_ThrowsIfMatrixIsNotSquare()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6);

            // Assert
            Assert.Throws<InvalidOperationException>(() => builder.Create());
        }

        [Fact]
        public void MatrixBuilder_CanConstructIdentityMatrix()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            var matrix = builder
                .AsIdentityMatrix(4)
                .Create();

            // Assert
            var expected = new MatrixBuilder()
                .WithRow(1, 0, 0, 0)
                .WithRow(0, 1, 0, 0)
                .WithRow(0, 0, 1, 0)
                .WithRow(0, 0, 0, 1)
                .Create();

            Assert.True(matrix.IsEqual(expected));
        }

        [Fact]
        public void MatrixBuilder_AsIdentityMatrix_ThrowsIfRowsHaveBeenAdded()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.WithRow(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.AsIdentityMatrix(3));
        }

        [Fact]
        public void MatrixBuilder_Reset_ClearsProvidedRowValues()
        {
            // Arrange
            var builder = new MatrixBuilder()
                .WithRow(1, 2, 3)
                .WithRow(4, 5, 6);

            // Act
            builder.Reset();

            // Assert
            var matrix = builder.Create();

            Assert.Equal(0, matrix.Size);
        }

        [Fact]
        public void MatrixBuilder_CanConstructTranslationMatrix()
        {
            // Arrange
            var point = new RayPoint(-3, 4, 5);

            // Act
            var transform = new MatrixBuilder()
                .AsTranslationMatrix(5, -3, 2)
                .Create();

            // Assert
            var expected = new RayPoint(2, 1, 7);
            var isEqual = expected.IsEqual(transform.Multiply(point)); 

            Assert.True(isEqual);
        }

        [Fact]
        public void MatrixBuilder_AsTranslationMatrix_ThrowsIfRowsHaveBeenAdded()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.WithRow(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.AsTranslationMatrix(1, 2, 3));
        }

        [Fact]
        public void MatrixBuilder_CanConstructScailingMatrix()
        {
            // Arrange
            var point = new RayPoint(-4, 6, 8);

            var transform = new MatrixBuilder()
                .AsScailingMatrix(2, 3, 4)
                .Create();

            // Act
            var scailed = transform.Multiply(point);

            // Assert
            var expected = new RayPoint(-8, 18, 32);
            var isEqual = expected.IsEqual(scailed);

            Assert.True(isEqual);
        }

        [Fact]
        public void MatrixBuilder_AsScailingMatrix_ThrowsIfRowsHaveBeenAdded()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.WithRow(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.AsScailingMatrix(1, 2, 3));
        }

        [Fact]
        public void MatrixBuilder_CanConstruct_X_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 1, 0);

            // Act
            var halfQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.X, (float)Math.PI / 4)
                .Create()
                .Multiply(point);

            var fullQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.X, (float)Math.PI / 2)
                .Create()
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);
            var expectedFull = new RayPoint(0, 0, 1);

            var halfIsEqual = halfQuarter.IsEqual(expectedHalf);
            var fullIsEqual = fullQuarter.IsEqual(expectedFull);

            Assert.True(halfIsEqual);
            Assert.True(fullIsEqual);        
        }

        [Fact]
        public void MatrixBuilder_AsRotationMatrix_ThrowsIfRowsHaveBeenAdded()
        {
            // Arrange
            var builder = new MatrixBuilder();

            // Act
            builder.WithRow(1, 2, 3);

            // Assert
            Assert.Throws<NotSupportedException>(() => builder.AsRotationMatrix(RotationAxis.X, 1));
        }

        [Fact]
        public void MatrixBuilder_CanConstruct_Y_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 0, 1);

            // Act
            var halfQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.Y, (float)Math.PI / 4)
                .Create()
                .Multiply(point);

            var fullQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.Y, (float)Math.PI / 2)
                .Create()
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint((float)Math.Sqrt(2) / 2, 0, (float)Math.Sqrt(2) / 2);
            var expectedFull = new RayPoint(1, 0, 0);

            var halfIsEqual = halfQuarter.IsEqual(expectedHalf);
            var fullIsEqual = fullQuarter.IsEqual(expectedFull);

            Assert.True(halfIsEqual);
            Assert.True(fullIsEqual);
        }

        [Fact]
        public void MatrixBuilder_CanConstruct_Z_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 1, 0);

            // Act
            var halfQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.Z, (float)Math.PI / 4)
                .Create()
                .Multiply(point);

            var fullQuarter = new MatrixBuilder()
                .AsRotationMatrix(RotationAxis.Z, (float)Math.PI / 2)
                .Create()
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint(-(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2, 0);
            var expectedFull = new RayPoint(-1, 0, 0);

            var halfIsEqual = halfQuarter.IsEqual(expectedHalf);
            var fullIsEqual = fullQuarter.IsEqual(expectedFull);

            Assert.True(halfIsEqual);
            Assert.True(fullIsEqual);
        }
    }
}
