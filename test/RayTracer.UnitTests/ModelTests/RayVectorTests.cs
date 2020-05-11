using System;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class RayVectorTests : BaseModelTests
    {
        [Fact]
        public void RayVector_CreatesBaseTupleWith_W_ValueOfZero()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            // Act
            var vector = new RayVector(x, y, z);
            var isVector = vector.IsVector();
            var isPoint = vector.IsPoint();

            // Assert
            Assert.True(isVector);
            Assert.False(isPoint);
        }

        [Theory]
        [InlineData(0, 1, 0, 1)]
        [InlineData(1, 2, 3, 14)]
        [InlineData(-1, -2, -3, 14)]
        public void RayVector_Magnitude_ReturnsTheCorrectValue(float x, float y, float z, float resultBeforeSqrt)
        {
            // Arrange
            var tuple = new RayVector(x, y, z);

            // Act
            var magnitude = tuple.Magnitude();

            // Assert
            var expected = Math.Sqrt(resultBeforeSqrt);
            Assert.Equal(expected, magnitude);
        }

        [Theory]
        [InlineData(4, 0, 0, 1, 0, 0)]
        [InlineData(1, 2, 3, 0.26726D, 0.53452D, 0.80178D)]
        public void RayVector_Normalise_ReturnsTheCorrectValue(
            float x, float y, float z,
            double expectedX, double expectedY, double expectedZ)
        {
            // Arrange
            var tuple = new RayVector(x, y, z);

            // Act
            var normalisedTuple = tuple.Normalise();

            // Assert
            Assert.Equal(expectedX, Math.Round(normalisedTuple.X, 5));
            Assert.Equal(expectedY, Math.Round(normalisedTuple.Y, 5));
            Assert.Equal(expectedZ, Math.Round(normalisedTuple.Z, 5));
        }

        [Fact]
        public void RayVector_Normalise_MagnitudeOfNormalisedTupleShouldBeOne()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var tuple = new RayVector(x, y, z);

            // Act
            var magnitude = tuple
                .Normalise()
                .Magnitude();

            // Assert
            Assert.Equal(1, Math.Round(magnitude, 5));
        }

        [Fact]
        public void RayVector_DotProduct_ReturnsTheCorrectValue()
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayVector(xOne, yOne, zOne);
            var tupleTwo = new RayVector(xTwo, yTwo, zTwo);

            // Act
            var dotProduct = tupleOne.DotProduct(tupleTwo);

            // Assert
            var expected = (tupleOne.X * tupleTwo.X) +
                           (tupleOne.Y * tupleTwo.Y) +
                           (tupleOne.Z * tupleTwo.Z) +
                           ((int)tupleOne.W * (int)tupleTwo.W);

            Assert.Equal(expected, dotProduct);
        }

        [Theory]
        [InlineData(1, 2, 3, 2, 3, 4, -1, 2, -1)]
        [InlineData(2, 3, 4, 1, 2, 3, 1, -2, 1)]
        public void RayVector_CrossProduct_ReturnsTheCorrectValue(
            float xOne, float yOne, float zOne,
            float xTwo, float yTwo, float zTwo,
            float expectedX, float expectedY, float expectedZ)
        {
            // Arrange
            var tupleOne = new RayVector(xOne, yOne, zOne);
            var tupleTwo = new RayVector(xTwo, yTwo, zTwo);

            // Act
            var crossProduct = tupleOne.CrossProduct(tupleTwo);

            // Assert
            var expected = new RayVector(expectedX, expectedY, expectedZ);

            Assert.Equal(expected.X, crossProduct.X);
            Assert.Equal(expected.Y, crossProduct.Y);
            Assert.Equal(expected.Z, crossProduct.Z);
        }
    }
}
