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
    }
}
