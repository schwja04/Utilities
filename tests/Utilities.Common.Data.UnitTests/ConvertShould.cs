using FluentAssertions;
using System;
using Xunit;

namespace Utilities.Common.Data.UnitTests
{
    public class ConvertShould
    {
        [Fact]
        public void Cast_WhenObjectIsNull_Throw_ArgumentNullException()
        {
            // Arrange
            var action = () => Convert.Cast<int>(null);

            // Act && Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("t", true)]
        [InlineData("yes", true)]
        [InlineData("y", true)]
        [InlineData(1, true)]
        [InlineData("false", false)]
        [InlineData("f", false)]
        [InlineData("no", false)]
        [InlineData("n", false)]
        [InlineData(0, false)]
        public void Cast_ToBoolean_Theories(object obj, bool expected)
        {
            // Arrange && Act && Assert
            Convert.Cast<bool>(obj).Should().Be(expected);
        }

        [Theory]
        [InlineData("hello     ")]
        [InlineData("     hello")]
        [InlineData("     hello     ")]
        public void Cast_ToString_Theories(string orig)
        {
            // Arrange
            string expected = orig.Trim();

            // Act
            string actual = Convert.Cast<string>(orig);

            // Assert
            string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase)
                .Should().BeTrue();
        }
    }
}

