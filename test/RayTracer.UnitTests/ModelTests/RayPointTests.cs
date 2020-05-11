using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class RayPointTests : BaseModelTests
    {
        [Fact]
        public void RayPoint_CreatesBaseTupleWith_W_ValueOfOne()
        {
            // Arrange
            var (x, y, z) = CreateRandomPosition(_fixture);

            // Act
            var point = new RayPoint(x, y, z);
            var isPoint = point.IsPoint();
            var isVector = point.IsVector();

            // Assert
            Assert.True(isPoint);
            Assert.False(isVector);
        }
    }
}
