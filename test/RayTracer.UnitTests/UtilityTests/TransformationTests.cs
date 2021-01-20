using System;
using RayTracer.Model;
using RayTracer.Utility;
using RayTracer.Utility.Model;
using Xunit;

namespace RayTracer.UnitTests.UtilityTests
{
    public class TransformationTests
    {
        [Fact]
        public void Transformation_GetIdentiyMatrix_ReturnsValidIdentityMatrix()
        {
            // Arrange / Act
            var matrix = Transformation.GetIdentiyMatrix(4);

            // Assert
            var expected = new MatrixBuilder()
                .WithRow(1, 0, 0, 0)
                .WithRow(0, 1, 0, 0)
                .WithRow(0, 0, 1, 0)
                .WithRow(0, 0, 0, 1)
                .Create();

            Assert.True(matrix.IsEqual(expected));
        }

        [Fact]
        public void Transformation_GetTranslationMatrix_ReturnsValidTranslationMatrix()
        {
            // Arrange
            var point = new RayPoint(-3, 4, 5);

            // Act
            var transform = Transformation
                .GetTranslationMatrix(5, -3, 2)
                .Multiply(point);

            // Assert
            var expected = new RayPoint(2, 1, 7);

            Assert.True(expected.IsEqual(transform));
        }

        [Fact]
        public void Transformation_GetScailingMatrix_ReturnsValidScailingMatrix()
        {
            // Arrange
            var point = new RayPoint(-4, 6, 8);

            var transform = Transformation.GetScailingMatrix(2, 3, 4);

            // Act
            var scailed = transform.Multiply(point);

            // Assert
            var expected = new RayPoint(-8, 18, 32);

            Assert.True(expected.IsEqual(scailed));
        }

        [Fact]
        public void Transformation_GetRotationMatrix_ReturnsValid_X_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 1, 0);

            // Act
            var halfQuarter = Transformation
                .GetRotationMatrix(RotationAxis.X, (float)Math.PI / 4)
                .Multiply(point);

            var fullQuarter = Transformation
                .GetRotationMatrix(RotationAxis.X, (float)Math.PI / 2)
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint(0, (float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2);
            var expectedFull = new RayPoint(0, 0, 1);

            Assert.True(halfQuarter.IsEqual(expectedHalf));
            Assert.True(fullQuarter.IsEqual(expectedFull));
        }

        [Fact]
        public void Transformation_GetRotationMatrix_ReturnsValid_Y_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 0, 1);

            // Act
            var halfQuarter = Transformation
                .GetRotationMatrix(RotationAxis.Y, (float)Math.PI / 4)
                .Multiply(point);

            var fullQuarter = Transformation
                .GetRotationMatrix(RotationAxis.Y, (float)Math.PI / 2)
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint((float)Math.Sqrt(2) / 2, 0, (float)Math.Sqrt(2) / 2);
            var expectedFull = new RayPoint(1, 0, 0);

            Assert.True(halfQuarter.IsEqual(expectedHalf));
            Assert.True(fullQuarter.IsEqual(expectedFull));
        }

        [Fact]
        public void Transformation_GetRotationMatrix_ReturnsValid_Z_RotationMatrix()
        {
            // Arrange
            var point = new RayPoint(0, 1, 0);

            // Act
            var halfQuarter = Transformation
                .GetRotationMatrix(RotationAxis.Z, (float)Math.PI / 4)
                .Multiply(point);

            var fullQuarter = Transformation
                .GetRotationMatrix(RotationAxis.Z, (float)Math.PI / 2)
                .Multiply(point);

            // Assert
            var expectedHalf = new RayPoint(-(float)Math.Sqrt(2) / 2, (float)Math.Sqrt(2) / 2, 0);
            var expectedFull = new RayPoint(-1, 0, 0);

            Assert.True(halfQuarter.IsEqual(expectedHalf));
            Assert.True(fullQuarter.IsEqual(expectedFull));
        }

        [Fact]
        public void Transformation_TranslationMatrices_DoNotAffectVectors()
        {
            // Arrange
            var vector = new RayVector(-3, 4, 5);

            // Act
            var transform = Transformation.GetTranslationMatrix(5, -3, 2);

            var translated = transform.Multiply(vector);

            // Assert
            Assert.True(vector.IsEqual(translated));
        }

        [Fact]
        public void Transformation_TranslationMatrices_AreInvertable()
        {
            // Arrange
            var point = new RayPoint(-3, 4, 5);

            var transform = Transformation.GetTranslationMatrix(5, -3, 2);

            // Act
            var invertedTransform = transform.Inverse();

            var transformed = invertedTransform.Multiply(point);

            // Assert
            var expected = new RayPoint(-8, 7, 3);

            Assert.True(expected.IsEqual(transformed));
        }

        [Fact]
        public void Transformation_ScailingMatrices_DoApplyToVectors()
        {
            // Arrange
            var vector = new RayVector(-4, 6, 8);

            // Act
            var transform = Transformation.GetScailingMatrix(2, 3, 4);

            var scailed = transform.Multiply(vector);

            // Assert
            var expected = new RayVector(-8, 18, 32);

            Assert.True(scailed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ScailingMatrices_AreInvertable()
        {
            // Arrange
            var vector = new RayVector(-4, 6, 8);

            var scail = Transformation.GetScailingMatrix(2, 3, 4);

            // Act
            var invertedScail = scail.Inverse();

            var transformed = invertedScail.Multiply(vector);

            // Assert
            var expected = new RayVector(-2, 2, 2);

            Assert.True(expected.IsEqual(transformed));
        }

        [Fact]
        public void Transformation_MatrixCanBeReflectedUsingScailing()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var reflection = Transformation.GetScailingMatrix(-1, 1, 1);

            // Act
            var reflected = reflection.Multiply(point);

            // Assert
            var expected = new RayPoint(-2, 3, 4);

            Assert.True(expected.IsEqual(reflected));
        }

        [Fact]
        public void Transformation_RotationMatrices_AreInvertable()
        {
            // Arrange
            var point = new RayPoint(0, 1, 0);

            var halfQuarter = Transformation
                .GetRotationMatrix(RotationAxis.X, (float)Math.PI / 4);

            // Act
            var invertedPoint = halfQuarter.Inverse().Multiply(point);

            // Assert
            var expected = new RayPoint(0, (float)Math.Sqrt(2) / 2, -(float)Math.Sqrt(2) / 2);

            Assert.True(invertedPoint.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_X_InProportionTo_Y()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var transform = new ShearingTransform { XinProportionToY = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(5, 3, 4);

            Assert.True(skewed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_X_InProportionTo_Z()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var transform = new ShearingTransform { XinProportionToZ = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(6, 3, 4);

            Assert.True(skewed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_Y_InProportionTo_X()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var transform = new ShearingTransform { YinProportionToX = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(2, 5, 4);

            Assert.True(skewed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_Y_InProportionTo_Z()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);
            
            var transform = new ShearingTransform { YinProportionToZ = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(2, 7, 4);

            Assert.True(skewed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_Z_InProportionTo_X()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var transform = new ShearingTransform { ZinProportionToX = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(2, 3, 6);

            Assert.True(skewed.IsEqual(expected));
        }

        [Fact]
        public void Transformation_ShearingMatrices_Move_Z_InProportionTo_Y()
        {
            // Arrange
            var point = new RayPoint(2, 3, 4);

            var transform = new ShearingTransform { ZinProportionToY = 1 };

            var shearing = Transformation.GetShearingMatrix(transform);

            // Act
            var skewed = point.Multiply(shearing);

            // Assert
            var expected = new RayPoint(2, 3, 7);

            Assert.True(skewed.IsEqual(expected));
        }
    }
}
