using FluentAssertions;
using Npgsql;
using System;
using Utilities.PSql.Data;
using Xunit;

namespace Utilities.PSql.UnitTests.Data
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

