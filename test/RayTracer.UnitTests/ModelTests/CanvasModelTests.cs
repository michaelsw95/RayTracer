using System;
using AutoFixture;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class CanvasModelTests
    {
        private Fixture _fixture;

        public CanvasModelTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Canvas_CanBeCreatedWithWidthAndHeight()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();

            // Act
            var canvas = new Canvas(width, height);

            // Assert
            Assert.Equal(width, canvas.Width);
            Assert.Equal(height, canvas.Height);
        }

        [Fact]
        public void Canvas_AllPixelsAreBlackAfterCreation()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();

            // Act
            var canvas = new Canvas(width, height);

            // Assert
            for (var i = 0; i < canvas.Width; i++)
            {
                for (int j = 0; j < canvas.Height; j++)
                {
                    var colour = canvas.GetPixel(i, j);

                    Assert.Equal(0, colour.Red);
                    Assert.Equal(0, colour.Green);
                    Assert.Equal(0, colour.Blue);
                }
            }
        }

        [Fact]
        public void Canvas_GetPixel_ThrowsIndexOutOfRangeExceptionIfIndexIsOutOfBounds()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();

            var canvas = new Canvas(width, height);

            // Act / Assert
            Assert.Throws<IndexOutOfRangeException>(() => canvas.GetPixel(width, height - 1));
            Assert.Throws<IndexOutOfRangeException>(() => canvas.GetPixel(width - 1, height));
        }

        [Fact]
        public void Canvas_SetPixel_CanWriteAColourToSpecificPixels()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();
            var colour = _fixture.Create<Colour>();
            var canvas = new Canvas(width, height);

            // Act
            canvas.SetPixel(colour, width - 1, height - 1);

            // Assert
            var colourFromCanvas = canvas.GetPixel(width - 1, height - 1);

            Assert.Equal(colour.Red, colourFromCanvas.Red);
            Assert.Equal(colour.Green, colourFromCanvas.Green);
            Assert.Equal(colour.Blue, colourFromCanvas.Blue);
        }

        [Fact]
        public void Canvas_SetPixel_ThrowsIndexOutOfRangeExceptionIfIndexIsOutOfBounds()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();
            var colour = _fixture.Create<Colour>();

            var canvas = new Canvas(width, height);

            // Act / Assert
            Assert.Throws<IndexOutOfRangeException>(() => canvas.SetPixel(colour, width, height - 1));
            Assert.Throws<IndexOutOfRangeException>(() => canvas.SetPixel(colour, width - 1, height));
        }
    }
}
