using System;
using AutoFixture;
using RayTracer.Model;
using RayTracer.Utility;
using RayTracer.Utility.Model;
using Xunit;

namespace RayTracer.UnitTests.UtilityTests
{
    public class TransformationBuilderTests
    {
        private readonly Fixture _fixture;

        public TransformationBuilderTests() => _fixture = new Fixture();

        [Fact]
        public void TransformationBuilder_GetTransformationMatrix_ThrowsIfNoTransformationsHaveBeenAdded()
        {
            // Arrange
            var builder = new TransformationBuilder();

            // Act / Assert
            Assert.Throws<InvalidOperationException>(() => builder.GetTransformationMatrix());
        }

        [Fact]
        public void TransformationBuilder_GetTransformationMatrix_ReturnsTheSameMatrixIfOnlyOneIsAdded()
        {
            // Arrange
            var matrix = GetRandomMatrix();
            var builder = new TransformationBuilder();

            // Act
            var result = builder
                .AddTransformation(matrix)
                .GetTransformationMatrix();

            // Assert
            Assert.True(matrix.IsEqual(result));
        }

        [Fact]
        public void TransformationBuilder_GetTransformationMatrix_ReturnsTheProductOfTwoMatricesIfBothAreAdded()
        {
            // Arrange
            var matrixOne = GetRandomMatrix();
            var matrixTwo = GetRandomMatrix(matrixOne.Size);

            var builder = new TransformationBuilder();

            // Act
            var result = builder
                .AddTransformation(matrixOne)
                .AddTransformation(matrixTwo)
                .GetTransformationMatrix();

            // Assert
            var expected = matrixTwo.Multiply(matrixOne);

            Assert.True(expected.IsEqual(result));
        }

        [Fact]
        public void TransformationBuilder_GetTransformationMatrix_ReturnsTheProductOfThreeMatricesIfAllAreAdded()
        {
            // Arrange
            var matrixOne = GetRandomMatrix();
            var matrixTwo = GetRandomMatrix(matrixOne.Size);
            var matrixThree = GetRandomMatrix(matrixOne.Size);

            var builder = new TransformationBuilder();

            // Act
            var result = builder
                .AddTransformation(matrixOne)
                .AddTransformation(matrixTwo)
                .AddTransformation(matrixThree)
                .GetTransformationMatrix();

            // Assert
            var expected = matrixThree.Multiply(matrixTwo).Multiply(matrixOne);

            Assert.True(expected.IsEqual(result));
        }

        [Fact]
        public void TransformationBuilder_ReturnsCorrectMatrixForMultipleTransforms()
        {
            // Arrange
            var point = new RayPoint(1, 0, 1);

            // Act
            var pointOne = Transformation.GetRotationMatrix(RotationAxis.X, (float)Math.PI / 2).Multiply(point);
            var pointTwo = Transformation.GetScailingMatrix(5, 5, 5).Multiply(pointOne);
            var resultOne = Transformation.GetTranslationMatrix(10, 5, 7).Multiply(pointTwo);

            var resultTwo = new TransformationBuilder()
                .AddRotation(RotationAxis.X, (float)Math.PI / 2)
                .AddScailing(5, 5, 5)
                .AddTranslation(10, 5, 7)
                .ApplyTransformations(point);

            // Assert
            var expected = new RayPoint(15, 0, 7);

            Assert.True(resultOne.IsEqual(expected));
            Assert.True(resultTwo.IsEqual(expected));
        }

        [Fact]
        public void TransformationBuilder_ReturnsTheSameResultAsIndividualTransfomationsAppliedInSequence()
        {
            // Arrange
            var point = new RayPoint(1, 0, 1);

            // Act
            var pointOne = Transformation.GetRotationMatrix(RotationAxis.X, (float)Math.PI / 2).Multiply(point);
            var pointTwo = Transformation.GetScailingMatrix(5, 5, 5).Multiply(pointOne);
            var pointThree = Transformation.GetShearingMatrix(new ShearingTransform { XinProportionToY = 1 }).Multiply(pointTwo);
            var resultOne = Transformation.GetTranslationMatrix(10, 5, 7).Multiply(pointThree);

            var resultTwo = new TransformationBuilder()
                .AddRotation(RotationAxis.X, (float)Math.PI / 2)
                .AddScailing(5, 5, 5)
                .AddShearing(new ShearingTransform { XinProportionToY = 1 })
                .AddTranslation(10, 5, 7)
                .ApplyTransformations(point);

            // Assert
            Assert.True(resultOne.IsEqual(resultTwo));
        }

        [Fact]
        public void TransformationBuilder_Reset_ClearsTheAddedTransformations()
        {
            // Arrange
            var matrix = GetRandomMatrix();
            
            var builder = new TransformationBuilder()
                .AddRotation(RotationAxis.X, (float)Math.PI / 2)
                .AddTranslation(10, 5, 7)
                .AddShearing(new ShearingTransform { YinProportionToX = 1 });

            // Act
            builder.Reset();

            // Assert
            Assert.Throws<InvalidOperationException>(() => builder.GetTransformationMatrix());

            var result = builder
                .AddTransformation(matrix)
                .GetTransformationMatrix();

            Assert.True(result.IsEqual(matrix));
        }


        private Matrix GetRandomMatrix(int? requiredSize = null)
        {
            var size = requiredSize ?? new Random().Next(4, 100);

            var randomMatrix = new Matrix(size);

            for (var i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var value = _fixture.Create<float>();

                    randomMatrix.Set(value, i, j);
                }
            }

            return randomMatrix;
        }
    }
}
