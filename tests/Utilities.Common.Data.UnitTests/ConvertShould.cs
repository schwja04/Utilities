using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Utilities.Common.Data;
using Xunit;

namespace Utilities.Common.Data.UnitTests
{
    public class ConvertShould
    {
        private readonly IFixture _fixture;

        private readonly Convert _convert;

        public ConvertShould()
        {
            _fixture = new Fixture();

            _convert = new Convert();
        }

        [Fact]
        public void Cast_ShouldConvertDecimalToInt()
        {
            // Arrange
            decimal orig = _fixture.Create<decimal>();
            int expected = (int)System.Convert.ChangeType(orig, typeof(int));

            // Act
            var actual = _convert.Cast<int>(orig);

            // Assert
            actual.Should().BeOfType(typeof(int));
            actual.Should().Be(expected);
        }

        [Fact]
        public void Cast_ShouldConvertIntToNullableInt()
        {
            // Arrange
            int orig = _fixture.Create<int>();
            int? expected = (int?)orig;

            // Act
            var actual = _convert.Cast<int?>(orig);

            // Assert
            actual.Should().HaveValue();
            actual.Should().NotBeNull();
            actual.Should().Be(expected);
        }

        [Fact]
        public void Cast_ShouldConvertValidOrdinalEnumValueToEnum()
        {
            // Arrange
            int orig = 1;
            var expected = (DateTimeKind)orig;

            // Act
            var actual = _convert.Cast<DateTimeKind>(orig);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Cast_ShouldConvertNullList()
        {
            // Arrange
            List<int> orig = null;

            // Act
            var actual = _convert.Cast<List<int>>(orig);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void ConvertWithStringHandler_ShouldProcessString()
        {
            // Arrange
            DefaultValueHandlerDictionary handlers = new();
            handlers.AddOrUpdate(typeof(string), value => value.ToString().Trim());

            Convert convert = new(handlers.DefaultValueHandlers);

            string orig = "   hello world   ";
            string excepted = orig.Trim();

            // Act
            var actual = convert.Cast<string>(orig);

            // Assert
            actual.Should().Be(excepted);
        }
    }
}

