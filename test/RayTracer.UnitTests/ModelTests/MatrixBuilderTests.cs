using System;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
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
            Assert.Equal(0, matrix.Rows);
            Assert.Equal(0, matrix.Columns);
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
                .WithRow(10, 11, 12)
                .Create();

            // Assert
            Assert.Equal(4, matrix.Rows);
            Assert.Equal(3, matrix.Columns);
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
    }
}
