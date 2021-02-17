using RayTracer.Model;
using RayTracer.Utility;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class SphereTests
    {
        [Fact]
        public void Sphere_HasDefaultTransformation_OfIdentityMatrix()
        {
            // Arrange / Act
            var identity = Transformation.GetIdentiyMatrix(
                Transformation.TransformationMatrixSize
            );

            var sphere = new Sphere();

            // Assert
            Assert.True(identity.IsEqual(sphere.Transform));
        }

        [Fact]
        public void Sphere_CanSetTransformation()
        {
            // Arrange
            var identity = Transformation.GetIdentiyMatrix(
                Transformation.TransformationMatrixSize
            );

            var translationMatrix = Transformation.GetTranslationMatrix(1, 2, 3);
            var sphere = new Sphere();

            // Act
            sphere.Transform = translationMatrix;

            // Assert
            Assert.False(identity.IsEqual(sphere.Transform));
            Assert.True(translationMatrix.IsEqual(sphere.Transform));
        }

        [Fact]
        public void Sphere_GetIntersections_ReturnsNoIntersections_WhenRaysMissSpheres()
        {
            // Arrange
            var sphere = new Sphere();
            var ray = new Ray(new RayPoint(0, 2, -5), new RayVector(0, 0, 1));

            // Act
            var intersections = sphere.GetIntersects(ray);

            // Assert
            Assert.Equal(0, intersections.Length);
        }

        [Fact]
        public void Sphere_GetIntersections_ReturnsTwoPoints_WhenRaysIntersectWithSpheres()
        {
            // Arrange
            var sphere = new Sphere();
            var ray = new Ray(new RayPoint(0, 0, -5), new RayVector(0, 0, 1));

            // Act
            var intersections = sphere.GetIntersects(ray);

            // Assert
            Assert.Equal(2, intersections.Length);

            foreach (var intersection in intersections)
            {
                Assert.Equal(sphere, intersection.Object);
            }

            Assert.True(Numeric.FloatIsEqual(4, intersections[0].Value));
            Assert.True(Numeric.FloatIsEqual(6, intersections[1].Value));
        }
    }
}
