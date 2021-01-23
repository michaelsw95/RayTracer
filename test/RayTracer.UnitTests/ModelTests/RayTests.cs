using AutoFixture;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class RayTests
    {
        private readonly Fixture _fixture;

        public RayTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Ray_CanConstructRay()
        {
            // Arrange
            var point = _fixture.Create<RayPoint>();
            var vector = _fixture.Create<RayVector>();

            // Act
            var ray = new Ray(point, vector);

            // Assert
            Assert.True(ray.Origin.IsEqual(point));
            Assert.True(ray.Direction.IsEqual(vector));
        }

        [Fact]
        public void Ray_GetPosition_ReturnsTheCorrectPosition()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);
            var vector = new RayVector(1, 0, 0);
            var ray = new Ray(point, vector);

            // Act
            var positionOne = ray.GetPosition(0);
            var positionTwo = ray.GetPosition(1);
            var positionThree = ray.GetPosition(-1);
            var positionFour = ray.GetPosition(2.5F);

            // Assert
            var expectedOne = new RayPoint(2, 3, 4);
            var expectedTwo = new RayPoint(3, 3, 4);
            var expectedThree = new RayPoint(1, 3, 4);
            var expectedFour = new RayPoint(4.5F, 3, 4);

            Assert.True(expectedOne.IsEqual(positionOne));
            Assert.True(expectedTwo.IsEqual(positionTwo));
            Assert.True(expectedThree.IsEqual(positionThree));
            Assert.True(expectedFour.IsEqual(positionFour));
        }
    }
}
