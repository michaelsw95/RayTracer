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

        [Fact]
        public void GetHit_ReturnsTheCorrectHit_WhenAllIntersectionsArePositive()
        {
            // Arrange
            var sphere = new Sphere();
            var intersectionOne = new Intersection(2, sphere);
            var intersectionTwo = new Intersection(4, sphere);
            var intersections = Intersection.Aggregate(intersectionOne, intersectionTwo);
            
            // Act
            var hit = Intersection.GetHit(intersections);

            // Assert
            Assert.Equal(intersectionOne, hit);
        }

        [Fact]
        public void GetHit_ReturnsTheCorrectHit_WhenSomeIntersectionsAreNegative()
        {
            // Arrange
            var sphere = new Sphere();
            var intersectionOne = new Intersection(-2, sphere);
            var intersectionTwo = new Intersection(3, sphere);
            var intersections = Intersection.Aggregate(intersectionOne, intersectionTwo);
            
            // Act
            var hit = Intersection.GetHit(intersections);

            // Assert
            Assert.Equal(intersectionTwo, hit);
        }

        [Fact]
        public void GetHit_ReturnsTheCorrectHit_WhenAllIntersectionsAreNegative()
        {
            // Arrange
            var sphere = new Sphere();
            var intersectionOne = new Intersection(-1, sphere);
            var intersectionTwo = new Intersection(-1, sphere);
            var intersections = Intersection.Aggregate(intersectionOne, intersectionTwo);
            
            // Act
            var hit = Intersection.GetHit(intersections);

            // Assert
            Assert.Null(hit);
        }

        [Fact]
        public void GetHit_ReturnsTheLowestNoneNegativeIntersection()
        {
            // Arrange
            var sphere = new Sphere();
            var intersectionOne = new Intersection(6, sphere);
            var intersectionTwo = new Intersection(9, sphere);
            var intersectionThree = new Intersection(-2, sphere);
            var intersectionFour = new Intersection(1, sphere);

            var intersections = Intersection.Aggregate(
                intersectionOne,
                intersectionTwo,
                intersectionThree,
                intersectionFour);
            
            // Act
            var hit = Intersection.GetHit(intersections);

            // Assert
            Assert.Equal(intersectionFour, hit);
        }

        private Fixture _fixture;
    }
}
