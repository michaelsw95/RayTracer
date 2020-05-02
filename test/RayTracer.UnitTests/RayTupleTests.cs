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

        private (float x, float y, float z) CreateRandomPosition(Fixture fixture) =>
            (fixture.Create<float>(), fixture.Create<float>(), fixture.Create<float>());
    }
}
