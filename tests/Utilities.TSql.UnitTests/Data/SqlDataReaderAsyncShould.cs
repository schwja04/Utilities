using FluentAssertions;
using Microsoft.Data.SqlClient;
using System;
using Utilities.TSql.Data;
using Xunit;

namespace Utilities.TSql.UnitTests.Data
{
    public class SqlDataReaderAsyncShould
    {
        [Fact]
        public void Ctor_ShouldThrowArgumentNullException()
        {
            // Arrange
            SqlDataReader reader = null;

            // Act
            var act = () => new SqlDataReaderAsync(reader);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}

