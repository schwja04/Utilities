using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Utilities.Common.Data.UnitTests
{
    public class DefaultValueHandlerDictionaryShould
    {
        private readonly IFixture _fixture;

        public DefaultValueHandlerDictionaryShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Ctor_WithNoParam_ShouldBeEmpty()
        {
            // Arrange && Act
            DefaultValueHandlerDictionary handlers = new();

            // Assert
            handlers.DefaultValueHandlers.Should().HaveCount(0);
        }

        [Fact]
        public void Ctor_WithParam_ShouldBeGreaterThanZero()
        {
            // Arrange
            var parameter = _fixture.Create<Dictionary<Type, Func<object, object>>>();

            // Act
            DefaultValueHandlerDictionary handlers = new(parameter);

            // Assert
            handlers.DefaultValueHandlers.Should().HaveCount(parameter.Count);
        }

        [Fact]
        public void AddOrUpdate_ShouldThrowArgumentNullException_WhenTypeIsNull()
        {
            // Arrange
            Type type = null;
            var handler = _fixture.Create<Func<object, object>>();

            DefaultValueHandlerDictionary handlers = new();

            // Act
            var act = () => handlers.AddOrUpdate(type, handler);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddOrUpdate_ShouldThrowArgumentNullException_WhenHandlerIsNull()
        {
            // Arrange
            var type = _fixture.Create<Type>();
            Func<object, object> handler = null;

            DefaultValueHandlerDictionary handlers = new();

            // Act
            var act = () => handlers.AddOrUpdate(type, handler);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddOrUpdate_ShouldBringHandlerCountUpToOne_WhenAdding()
        {
            // Arrange
            var type = _fixture.Create<Type>();
            var handler = _fixture.Create<Func<object, object>>();

            DefaultValueHandlerDictionary handlers = new();

            // Act && Assert
            handlers.DefaultValueHandlers.Should().HaveCount(0);

            handlers.AddOrUpdate(type, handler);
            handlers.DefaultValueHandlers.Should().HaveCount(1);
        }

        [Fact]
        public void AddOrUpdate_HandlerCountShouldRemainAtOne_WhenUpdating()
        {
            // Arrange
            var type = _fixture.Create<Type>();
            var handler = _fixture.Create<Func<object, object>>();

            DefaultValueHandlerDictionary handlers = new();
            handlers.AddOrUpdate(type, handler);

            // Act
            handlers.AddOrUpdate(type, handler);
            handlers.AddOrUpdate(type, handler);
            handlers.AddOrUpdate(type, handler);

            // Assert
            handlers.DefaultValueHandlers.Should().HaveCount(1);
        }

        [Fact]
        public void AddOrUpdateMany_HandlerCountShouldBeGreaterThanOrEqualToOne()
        {
            // Arrange
            var newHandlers = _fixture.Create<Dictionary<Type, Func<object, object>>>();

            DefaultValueHandlerDictionary handlers = new();

            // Act
            handlers.AddOrUpdateMany(newHandlers);

            // Assert
            handlers.DefaultValueHandlers.Should().HaveCountGreaterThanOrEqualTo(1);
            handlers.DefaultValueHandlers.Should().HaveCount(newHandlers.Count);
        }

        [Fact]
        public void Clear_ShouldEmptyOut_Dictionary()
        {
            // Arrange
            var newHandlers = _fixture.CreateMany<KeyValuePair<Type, Func<object, object>>>();
            DefaultValueHandlerDictionary handlers = new(newHandlers);

            // Act && Assert
            handlers.DefaultValueHandlers.Count.Should().BeGreaterThan(0);

            handlers.Clear();

            handlers.DefaultValueHandlers.Count.Should().Be(0);
        }

        [Fact]
        public void TryGetValue_ShouldReturn_TrueAndFunc()
        {
            // Arrange
            var newHandlers = _fixture.CreateMany<KeyValuePair<Type, Func<object, object>>>(count: 1).ToArray();
            DefaultValueHandlerDictionary handlers = new(newHandlers);

            // Act
            bool found = handlers.TryGetValue(newHandlers[0].Key, out Func<object, object> actual);

            // Assert
            found.Should().BeTrue();
            actual.Should().BeOfType<Func<object, object>>();
        }

        [Fact]
        public void TryGetValue_ShouldReturn_FalseAndNull()
        {
            // Arrange
            var key = _fixture.Create<Type>();
            DefaultValueHandlerDictionary handlers = new();

            // Act
            bool found = handlers.TryGetValue(key, out Func<object, object> actual);

            // Assert
            found.Should().BeFalse();
            actual.Should().BeNull();
        }
    }
}
