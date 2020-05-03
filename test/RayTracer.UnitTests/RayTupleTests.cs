using System;
using AutoFixture;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests
{
    public class RayTupleTests
    {
        private readonly Fixture _fixture;

        public RayTupleTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void RayTuple_With_W_ValueOfZeroIsAVector()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var w = 0;

            // Act
            var tuple = new RayTuple(x, y, z, (RayTupleType)w);

            // Assert
            Assert.Equal(RayTupleType.Vector, tuple.W);
        }

        [Fact]
        public void RayTuple_With_W_ValueOfOneIsAPoint()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var w = 1;

            // Act
            var tuple = new RayTuple(x, y, z, (RayTupleType)w);

            // Assert
            Assert.Equal(RayTupleType.Point, tuple.W);
        }

        [Fact]
        public void RayVector_CreatesBaseTupleWith_W_ValueOfZero()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            // Act
            var vector = new RayVector(x, y, z);

            // Assert
            Assert.Equal((RayTupleType)0, vector.W);
        }

        [Fact]
        public void RayPoint_CreatesBaseTupleWith_W_ValueOfOne()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            // Act
            var point = new RayPoint(x, y, z);

            // Assert
            Assert.Equal((RayTupleType)1, point.W);
        }

        [Fact]
        public void RayTuple_IsEqual_ReturnsTrueIfTuplesAreEqual()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            var tupleOne = new RayVector(x, y, z);
            var tupleTwo = new RayVector(x, y, z);

            // Act
            var isEqual = tupleOne.IsEqual(tupleTwo);
            
            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void RayTuple_IsEqual_ReturnsTrueIfTuplePositionValuesAreDifferentByLessThanFiveDP()
        {
            // Arrange
            var (x, y, _) = CreateRandomPosition(_fixture);
            var zOne = 1.23456F;
            var zTwo = zOne + 0.000005F;

            var tupleOne = new RayVector(x, y, zOne);
            var tupleTwo = new RayVector(x, y, zTwo);

            // Act
            var isEqual = tupleOne.IsEqual(tupleTwo);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void RayTuple_IsEqual_ReturnsFalseIfTuplesPositionValuesAreDifferent()
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayPoint(xOne, yOne, zOne);
            var tupleTwo = new RayPoint(xTwo, yTwo, zTwo);

            // Act
            var isEqual = tupleOne.IsEqual(tupleTwo);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void RayTuple_IsEqual_ReturnsFalseIfTuples_W_ValuesAreDifferent()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            var tupleOne = new RayPoint(x, y, z);
            var tupleTwo = new RayVector(x, y, z);

            // Act
            var isEqual = tupleOne.IsEqual(tupleTwo);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void RayTuple_Add_ReturnsANewTupleWithPropertiesSummedTogether()
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayPoint(xOne, yOne, zOne);
            var tupleTwo = new RayVector(xTwo, yTwo, zTwo);

            // Act
            var summedTuple = tupleOne.Add(tupleTwo);

            // Assert
            Assert.Equal(xOne + xTwo, summedTuple.X);
            Assert.Equal(yOne + yTwo, summedTuple.Y);
            Assert.Equal(zOne + zTwo, summedTuple.Z);
            Assert.Equal((RayTupleType)((int)tupleOne.W + (int)tupleTwo.W), summedTuple.W);
        }

        [Theory]
        [InlineData(RayTupleType.Vector, RayTupleType.Vector, typeof(RayVector))]
        [InlineData(RayTupleType.Vector, RayTupleType.Point, typeof(RayPoint))]
        public void RayTuple_Add_ReturnsTheCorrectType(RayTupleType wOne, RayTupleType wTwo, Type type)
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayTuple(xOne, yOne, zOne, wOne);
            var tupleTwo = new RayTuple(xTwo, yTwo, zTwo, wTwo);

            // Act
            var summedTuple = tupleOne.Add(tupleTwo);

            // Assert
            Assert.IsType(type, summedTuple);
        }

        [Fact]
        public void RayTuple_Subtract_ReturnsANewTupleWithPropertiesSubtracted()
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayPoint(xOne, yOne, zOne);
            var tupleTwo = new RayVector(xTwo, yTwo, zTwo);

            // Act
            var subtractedTuple = tupleOne.Subtract(tupleTwo);

            // Assert
            Assert.Equal(xOne - xTwo, subtractedTuple.X);
            Assert.Equal(yOne - yTwo, subtractedTuple.Y);
            Assert.Equal(zOne - zTwo, subtractedTuple.Z);
            Assert.Equal((RayTupleType)(tupleOne.W - tupleTwo.W), subtractedTuple.W);
        }

        [Theory]
        [InlineData(RayTupleType.Vector, RayTupleType.Vector, typeof(RayVector))]
        [InlineData(RayTupleType.Point, RayTupleType.Vector, typeof(RayPoint))]
        public void RayTuple_Subtract_ReturnsTheCorrectType(RayTupleType wOne, RayTupleType wTwo, Type type)
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);
            var (xTwo, yTwo, zTwo) = CreateRandomPosition(_fixture);

            var tupleOne = new RayTuple(xOne, yOne, zOne, wOne);
            var tupleTwo = new RayTuple(xTwo, yTwo, zTwo, wTwo);

            // Act
            var summedTuple = tupleOne.Subtract(tupleTwo);

            // Assert
            Assert.IsType(type, summedTuple);
        }

        [Theory]
        [InlineData(RayTupleType.Point)]
        [InlineData(RayTupleType.Vector)]
        public void RayTuple_Negate_ReturnsANewNegatedTuple(RayTupleType w)
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var tuple = new RayTuple(x, y, z, w);

            // Act
            var negatedTuple = tuple.Negate();

            // Assert
            Assert.Equal(x * -1, negatedTuple.X);
            Assert.Equal(y * -1, negatedTuple.Y);
            Assert.Equal(z * -1, negatedTuple.Z);
            Assert.Equal((RayTupleType)((int)w * -1), negatedTuple.W);
        }

        [Fact]
        public void RayTuple_Multiply_ReturnsANewTupleWithPropertiesMultipliedByScaler()
        {
            // Arrange
            var (xOne, yOne, zOne) = CreateRandomPosition(_fixture);

            var tuple = new RayPoint(xOne, yOne, zOne);

            // Act
            var multipliedTuple = tuple.Multiply(1.5F);

            // Assert
            Assert.Equal(xOne * 1.5F, multipliedTuple.X);
            Assert.Equal(yOne * 1.5F, multipliedTuple.Y);
            Assert.Equal(zOne * 1.5F, multipliedTuple.Z);
            Assert.Equal((RayTupleType)((int)tuple.W * 1.5F), multipliedTuple.W);
        }

        [Theory]
        [InlineData((RayTupleType)4, 0.25F, typeof(RayPoint))]
        [InlineData(RayTupleType.Vector, 2F, typeof(RayVector))]
        public void RayTuple_Multiply_ReturnsTheCorrectType(RayTupleType w, float scaler, Type type)
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var tuple = new RayTuple(x, y, z, w);

            // Act
            var multipliedTuple = tuple.Multiply(scaler);

            // Assert
            Assert.IsType(type, multipliedTuple);
        }

        [Theory]
        [InlineData(0, 1, 0, 1)]
        [InlineData(1, 2, 3, 14)]
        [InlineData(-1, -2, -3, 14)]
        public void RayTuple_Magnitude_ReturnsTheCorrectValue(float x, float y, float z, float resultBeforeSqrt)
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
        public void RayTuple_Normalise_ReturnsTheCorrectValue(
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
        public void RayTuple_Normalise_MagnitudeOfNormalisedTupleShouldBeOne()
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
        public void RayTuple_DotProduct_ReturnsTheCorrectValue()
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
        public void RayTuple_CrossProduct_ReturnsTheCorrectValue(
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

        private (float x, float y, float z) CreateRandomPosition(Fixture fixture) =>
            (fixture.Create<float>(), fixture.Create<float>(), fixture.Create<float>());
    }
}
