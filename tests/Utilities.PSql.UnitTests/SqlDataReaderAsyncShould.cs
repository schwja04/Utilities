using FluentAssertions;
using Npgsql;
using System;
using Xunit;

namespace Utilities.PSql.UnitTests
{
    public class SqlDataReaderAsyncShould
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException()
        {
            // Arrange
            NpgsqlDataReader reader = null;

            // Act
            var act = () => new SqlDataReaderAsync(reader);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}

