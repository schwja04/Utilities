using FluentAssertions;
using Microsoft.Data.Sqlite;
using System;
using Utilities.Sqlite;
using Utilities.Sqlite.Data;
using Xunit;

namespace Utilities.Sqlite.UnitTests.Data
{
    public class SqlDataReaderAsyncShould
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException()
        {
            // Arrange
            SqliteDataReader reader = null;

            // Act
            var act = () => new SqlDataReaderAsync(reader);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}

