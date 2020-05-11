using System;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class RayTupleTests : BaseModelTests
    {
        [Fact]
        public void RayTuple_With_W_ValueOfZeroIsAVector()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var w = 0;

            // Act
            var tuple = new RayTuple(x, y, z, w);
            var isVector = tuple.IsVector();
            var isPoint = tuple.IsPoint();

            // Assert
            Assert.True(isVector);
            Assert.False(isPoint);
        }

        [Fact]
        public void RayTuple_With_W_ValueOfOneIsAPoint()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var w = 1;

            // Act
            var tuple = new RayTuple(x, y, z, w);
            var isPoint = tuple.IsPoint();
            var isVector = tuple.IsVector();

            // Assert
            Assert.True(isPoint);
            Assert.False(isVector);
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
            Assert.Equal(tupleOne.W + tupleTwo.W, summedTuple.W);
        }

        [Theory]
        [InlineData(0, 0, typeof(RayVector))]
        [InlineData(0, 1, typeof(RayPoint))]
        public void RayTuple_Add_ReturnsTheCorrectType(float wOne, float wTwo, Type type)
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
            Assert.Equal(tupleOne.W - tupleTwo.W, subtractedTuple.W);
        }

        [Theory]
        [InlineData(0, 0, typeof(RayVector))]
        [InlineData(1, 0, typeof(RayPoint))]
        public void RayTuple_Subtract_ReturnsTheCorrectType(float wOne, float wTwo, Type type)
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
        [InlineData(1)]
        [InlineData(0)]
        public void RayTuple_Negate_ReturnsANewNegatedTuple(float w)
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
            Assert.Equal((w * -1), negatedTuple.W);
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
            Assert.Equal(tuple.W * 1.5F, multipliedTuple.W);
        }

        [Theory]
        [InlineData(4, 0.25F, typeof(RayPoint))]
        [InlineData(0, 2F, typeof(RayVector))]
        public void RayTuple_Multiply_ReturnsTheCorrectType(float w, float scaler, Type type)
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);
            var tuple = new RayTuple(x, y, z, w);

            // Act
            var multipliedTuple = tuple.Multiply(scaler);

            // Assert
            Assert.IsType(type, multipliedTuple);
        }

        [Fact]
        public void RayTuple_Multiply_CanTakeMatrixAndReturnTupleMultipliedByMatrixComponenents()
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
            var multipliedTuple = tuple.Multiply(matrix);

            // Assert
            var expected = new RayTuple(18, 24, 33, 1);
            var isEqual = expected.IsEqual(multipliedTuple);

            Assert.True(isEqual);
        }
    }
}
