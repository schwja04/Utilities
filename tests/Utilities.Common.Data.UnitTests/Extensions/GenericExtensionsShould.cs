using FluentAssertions;
using Utilities.Common.Data.Extensions;
using Xunit;

namespace Utilities.Common.Data.UnitTests.Extensions
{
    public class GenericExtensionsShould
    {
        [Fact]
        public void GetDefaultValueShould()
        {
            // Arrange
            var expectedInt = default(int);
            var expectedString = string.Empty;
            var expectedBool = default(bool);

            // Act
            var actualInt = GenericExtensions.GetDefaultValue<int>();
            var actualString = GenericExtensions.GetDefaultValue<string>();
            var actualBool = GenericExtensions.GetDefaultValue<bool>();

            // Assert
            actualInt.Should().Be(expectedInt);
            actualString.Should().Be(expectedString);
            actualBool.Should().Be(expectedBool);
        }
    }
}
