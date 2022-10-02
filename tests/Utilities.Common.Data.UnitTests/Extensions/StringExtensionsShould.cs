using FluentAssertions;
using Utilities.Common.Data.Extensions;
using Xunit;

namespace Utilities.Common.Data.UnitTests.Extensions
{
    public class StringExtensionsShould
    {
        [Theory]
        [InlineData("true", true, true)]
        [InlineData("t", true, true)]
        [InlineData("yes", true, true)]
        [InlineData("y", true, true)]
        [InlineData("1", true, true)]
        [InlineData("false", false, true)]
        [InlineData("f", false, true)]
        [InlineData("no", false, true)]
        [InlineData("n", false, true)]
        [InlineData("0", false, true)]
        [InlineData("", false, false)]
        [InlineData("hello", false, false)]
        public void TryParseBoolean_Theories(string data, bool expectedResult, bool expectedParseSuccess)
        {
            // Arrange
            bool result = false;

            // Act
            bool actualParse = StringExtensions.TryParseBoolean(data, ref result);

            // Assert
            actualParse.Should().Be(expectedParseSuccess);
            result.Should().Be(expectedResult);
        }
    }
}
