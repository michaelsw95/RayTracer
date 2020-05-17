using AutoFixture;
using RayTracer.Utility;
using Xunit;

namespace RayTracer.UnitTests.UtilityTests
{
    public class NumericTests
    {
        public NumericTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Numeric_FloatIsEqual_ReturnsFalseForNumbersWhichAreDifferent()
        {
            // Arrange
            var numberOne = _fixture.Create<float>();
            var numberTwo = numberOne + _fixture.Create<float>();

            // Act
            var isEqual = Numeric.FloatIsEqual(numberOne, numberTwo);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void Numeric_FloatIsEqual_ReturnsTrueForNumbersWhichAreTheSame()
        {
            // Arrange
            var number = _fixture.Create<float>();

            // Act
            var isEqual = Numeric.FloatIsEqual(number, number);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void Numeric_FloatIsEqual_ReturnsTrueForNumbersWhichWithinFiveDecimalPlaces()
        {
            // Arrange
            var numberOne = _fixture.Create<float>();
            var numberTwo = numberOne + 0.000001F;

            // Act
            var isEqual = Numeric.FloatIsEqual(numberOne, numberTwo);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void Numeric_Clamp_LimitsValuesToTheUpperBound()
        {
            // Arrange
            var upperBound = _fixture.Create<int>();
            var value = upperBound + _fixture.Create<int>();

            // Act
            var clampedNumber = Numeric.Clamp(0, upperBound, value);

            // Assert
            Assert.Equal(upperBound, clampedNumber);
        }

        [Fact]
        public void Numeric_Clamp_LimitsValuesToTheLowerBound()
        {
            // Arrange
            var lowerBound = _fixture.Create<int>();
            var upperBound = lowerBound + _fixture.Create<int>();
            var value = lowerBound - _fixture.Create<int>();

            // Act
            var clampedNumber = Numeric.Clamp(lowerBound, upperBound, value);

            // Assert
            Assert.Equal(lowerBound, clampedNumber);
        }

        [Fact]
        public void Numeric_IsWithinRange_ReturnsFalseIfOutOfRange()
        {
            // Arrange
            var lowerBound = _fixture.Create<int>();
            var upperBound = lowerBound + _fixture.Create<int>();
            var valueLower = lowerBound - _fixture.Create<int>();
            var valueUpper = upperBound + _fixture.Create<int>();

            // Act
            var lowerIsWithinRange = Numeric
                .IsWithinRange(lowerBound, upperBound, valueLower);

            var upperIsWithinRange = Numeric
                .IsWithinRange(lowerBound, upperBound, valueUpper);

            // Assert
            Assert.False(lowerIsWithinRange);
            Assert.False(upperIsWithinRange);
        }

        [Fact]
        public void Numeric_IsWithinRange_ReturnsTrueIfWithinRange()
        {
            // Arrange
            var lowerBound = _fixture.Create<int>();
            var upperBound = lowerBound + _fixture.Create<int>();
            var valueLower = lowerBound + 1;
            var valueUpper = upperBound - 1;

            // Act
            var lowerIsWithinRange = Numeric
                .IsWithinRange(lowerBound, upperBound, valueLower);

            var upperIsWithinRange = Numeric
                .IsWithinRange(lowerBound, upperBound, valueUpper);

            // Assert
            Assert.True(lowerIsWithinRange);
            Assert.True(upperIsWithinRange);
        }

        private Fixture _fixture;
    }
}
