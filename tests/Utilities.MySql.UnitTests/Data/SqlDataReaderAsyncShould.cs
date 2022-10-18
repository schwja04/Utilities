using FluentAssertions;
using MySql.Data.MySqlClient;
using System;
using Utilities.MySql.Data;
using Xunit;

namespace Utilities.MySql.UnitTests.Data
{
    public class SqlDataReaderAsyncShould
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException()
        {
            // Arrange
            MySqlDataReader reader = null;

            // Act
            var act = () => new SqlDataReaderAsync(reader);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}

