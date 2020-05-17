using AutoFixture;
using RayTracer.Model;
using Xunit;

namespace RayTracer.UnitTests.ModelTests
{
    public class ColourTests
    {
        private readonly Fixture _fixture;

        public ColourTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Colour_CanSetRedGreenAndBlueAttributes()
        {
            // Arrange
            var (red, green, blue) = GetRandomColour(_fixture);

            // Act
            var colour = new Colour(red, green, blue);

            // Assert
            Assert.Equal(red, colour.Red);
            Assert.Equal(green, colour.Green);
            Assert.Equal(blue, colour.Blue);
        }

        [Fact]
        public void Colour_Add_ReturnsANewColourWithColoursSummedTogether()
        {
            // Arrange
            var (redOne, greenOne, blueOne) = GetRandomColour(_fixture);
            var (redTwo, greenTwo, blueTwo) = GetRandomColour(_fixture);

            var colourOne = new Colour(redOne, greenOne, blueOne);
            var colourTwo = new Colour(redTwo, greenTwo, blueTwo);

            // Act
            var summedColour = colourOne.Add(colourTwo);

            // Assert
            Assert.IsType<Colour>(summedColour);
            Assert.Equal(colourOne.Red + colourTwo.Red, summedColour.Red);
            Assert.Equal(colourOne.Green + colourTwo.Green, summedColour.Green);
            Assert.Equal(colourOne.Blue + colourTwo.Blue, summedColour.Blue);
        }

        [Fact]
        public void Colour_Subtract_ReturnsANewColourWithColoursSubtracted()
        {
            // Arrange
            var (redOne, greenOne, blueOne) = GetRandomColour(_fixture);
            var (redTwo, greenTwo, blueTwo) = GetRandomColour(_fixture);

            var colourOne = new Colour(redOne, greenOne, blueOne);
            var colourTwo = new Colour(redTwo, greenTwo, blueTwo);

            // Act
            var subtractedColour = colourOne.Subtract(colourTwo);

            // Assert
            Assert.IsType<Colour>(subtractedColour);
            Assert.Equal(colourOne.Red - colourTwo.Red, subtractedColour.Red);
            Assert.Equal(colourOne.Green - colourTwo.Green, subtractedColour.Green);
            Assert.Equal(colourOne.Blue - colourTwo.Blue, subtractedColour.Blue);
        }

        [Fact]
        public void Colour_Multiply_ReturnsANewColourWithColoursMultipliedByScaler()
        {
            // Arrange
            var (red, green, blue) = GetRandomColour(_fixture);
            var colour = new Colour(red, green, blue);
            var scaler = _fixture.Create<float>();

            // Act
            var multipliedColour = colour.Multiply(scaler);

            // Assert
            Assert.Equal(red * scaler, multipliedColour.Red);
            Assert.Equal(green * scaler, multipliedColour.Green);
            Assert.Equal(blue * scaler, multipliedColour.Blue);
        }

        [Fact]
        public void Colour_HadamardProduct_ReturnsANewColourWithColoursMultipliedTogether()
        {
            // Arrange
            var (redOne, greenOne, blueOne) = GetRandomColour(_fixture);
            var (redTwo, greenTwo, blueTwo) = GetRandomColour(_fixture);

            var colourOne = new Colour(redOne, greenOne, blueOne);
            var colourTwo = new Colour(redTwo, greenTwo, blueTwo);

            // Act
            var multipliedColour = colourOne.HadamardProduct(colourTwo);

            // Assert
            Assert.IsType<Colour>(multipliedColour);
            Assert.Equal(colourOne.Red * colourTwo.Red, multipliedColour.Red);
            Assert.Equal(colourOne.Green * colourTwo.Green, multipliedColour.Green);
            Assert.Equal(colourOne.Blue * colourTwo.Blue, multipliedColour.Blue);
        }

        private (float red, float green, float blue) GetRandomColour(Fixture fixture)
        {
            return (fixture.Create<float>(),
                    fixture.Create<float>(),
                    fixture.Create<float>());
        }
    }
}
