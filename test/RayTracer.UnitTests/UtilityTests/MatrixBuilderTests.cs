using System;
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
    }
}
