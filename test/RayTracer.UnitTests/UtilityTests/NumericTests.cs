using System;
using System.Collections.Generic;
using System.Text;
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

        private Fixture _fixture;
    }
}
