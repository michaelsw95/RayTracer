using AutoFixture;
using RayTracer.Model;
using RayTracer.Utility;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class IntersectionTests
    {        
        public IntersectionTests() => _fixture = new Fixture();
        
        [Fact]
        public void Intersection_EncapsulateTheWorldObjectAndValue()
        {
            // Arrange
            var value = _fixture.Create<float>();
            var worldObject = new Sphere();

            // Act
            var intersection = new Intersection(value, worldObject);

            // Assert
            Assert.Equal(worldObject, intersection.Object);
            Assert.True(Numeric.FloatIsEqual(value, intersection.Value));
        }

        [Fact]
        public void Intersection_Aggregate_ReturnsAnArrayOfAllPassedInIntersections()
        {
            // Arrange
            var intersectionOne = _fixture.Create<Intersection>();
            var intersectionTwo = _fixture.Create<Intersection>();
            var intersectionThree = _fixture.Create<Intersection>();
            
            // Act
            var aggregationOne = Intersection.Aggregate(intersectionOne);
            var aggregationTwo = Intersection.Aggregate(intersectionOne, intersectionTwo);
            var aggregationThree = Intersection.Aggregate(intersectionOne, intersectionTwo, intersectionThree);

            // Assert
            Assert.Equal(1, aggregationOne.Length);
            Assert.Equal(2, aggregationTwo.Length);
            Assert.Equal(3, aggregationThree.Length);
        }

        private Fixture _fixture;
    }
}
