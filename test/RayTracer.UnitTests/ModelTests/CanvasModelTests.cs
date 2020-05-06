using System;
using System.Linq;
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

        [Fact]
        public void Canvas_ConvertToPPM_ReturnsStringWith_PPM_FileHeader()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();

            var canvas = new Canvas(width, height);

            // Act
            var ppmString = canvas.ConvertToPPM();

            // Assert
            var headerLines = ppmString
                .Split(Environment.NewLine)
                .Take(3)
                .ToArray();

            Assert.Equal(3, headerLines.Length);
            Assert.Equal("P3", headerLines[0]);
            Assert.Equal($"{width} {height}", headerLines[1]);
            Assert.Equal("255", headerLines[2]);
        }

        [Fact]
        public void Canvas_ConvertToPPM_ReturnsStringWith_PPM_PixelData()
        {
            // Arrange
            var canvas = new Canvas(5, 3);
            var colourOne = new Colour(1.5F, 0, 0);
            var colourTwo = new Colour(0, 0.5F, 0);
            var colourThree = new Colour(-0.5F, 0, 1);

            canvas.SetPixel(colourOne, 0, 0);
            canvas.SetPixel(colourTwo, 2, 1);
            canvas.SetPixel(colourThree, 4, 2);

            // Act
            var ppmString = canvas.ConvertToPPM();

            // Assert
            var pixelData = ppmString
                .Split(Environment.NewLine)
                .Skip(3)
                .ToArray();

            Assert.Equal(4, pixelData.Length);
            Assert.Equal("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", pixelData[0]);
            Assert.Equal("0 0 0 0 0 0 0 128 0 0 0 0 0 0 0", pixelData[1]);
            Assert.Equal("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", pixelData[2]);
        }

        [Fact]
        public void Canvas_ConvertToPPM_Returns_PPM_StringTerminatedWithNewLine()
        {
            // Arrange
            var width = _fixture.Create<int>();
            var height = _fixture.Create<int>();

            var canvas = new Canvas(width, height);

            // Act
            var ppmString = canvas.ConvertToPPM();

            // Assert
            var newLineLength = Environment.NewLine.Length;
            var lastCharacters = ppmString.Substring(ppmString.Length - newLineLength);

            Assert.Equal(Environment.NewLine, lastCharacters);
        }
    }
}
