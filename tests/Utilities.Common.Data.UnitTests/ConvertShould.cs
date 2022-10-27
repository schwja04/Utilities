using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Utilities.Common.Data;
using Xunit;

namespace Utilities.Common.Data.UnitTests
{
    public class ConvertShould
    {
        private readonly IFixture _fixture;

        public ConvertShould()
        {
            _fixture = new Fixture();
        }

        // TODO: This fails because `Convert` holds state and we are not guaranteed run order of unit tests.
        // I would like to find an alternate implementation for this.
        //[Fact]
        //public void DefaultValueHandlers_ShouldBeNullBeforeInitialization()
        //{
        //    // Arrange && Act
        //    var actual = Convert.DefaultValueHandlers;

        //    // Assert
        //    actual.Should().BeNull();
        //}

        [Fact]
        public void AddOrUpdateHandler_ShouldThrowArgumentNullException_WhenTypeIsNull()
        {
            // Arrange
            Type type = null;
            var handler = _fixture.Create<Func<object, object>>();

            // Act
            var act = () => Convert.AddOrUpdateHandler(type, handler);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(type));
        }

        [Fact]
        public void AddOrUpdateHandler_ShouldThrowArgumentNullException_WhenHandlerIsNull()
        {
            // Arrange
            var type = _fixture.Create<Type>();
            Func<object, object> handler = null;

            // Act
            var act = () => Convert.AddOrUpdateHandler(type, handler);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(handler));
        }

        [Fact]
        public void AddOrUpdateHandlers_ShouldThrowArgumentNullException_WhenTypeIsNull()
        {
            // Arrange
            Type type = null;
            var handler = _fixture.Create<Func<object, object>>();
            var typeHandlerList = new List<KeyValuePair<Type, Func<object, object>>>(1)
            {
                new(type, handler)
            };

            // Act
            var act = () => Convert.AddOrUpdateHandlers(typeHandlerList);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(type));
        }

        [Fact]
        public void AddOrUpdateHandlers_ShouldThrowArgumentNullException_WhenHandlerIsNull()
        {
            // Arrange
            var type = _fixture.Create<Type>();
            Func<object, object> handler = null;
            var typeHandlerList = new List<KeyValuePair<Type, Func<object, object>>>(1)
            {
                new(type, handler)
            };

            // Act
            var act = () => Convert.AddOrUpdateHandlers(typeHandlerList);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithParameterName(nameof(handler));
        }

        [Fact]
        public void Cast_ToNullableInt_ShouldNotThrow_WhenNullIsPassed()
        {
            // Arrange && Act
            var actual = Convert.Cast<int?>(null);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Cast_ToReferenceType()
        {
            // Arrange && Act
            var actual = Convert.Cast<List<int>>(null);

            // Assert
            actual.Should().BeNull();
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

