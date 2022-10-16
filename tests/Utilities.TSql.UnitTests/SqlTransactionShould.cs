using AutoFixture;
using FluentAssertions;
using System;
using Utilities.Common.Sql.Abstractions;
using Utilities.TSql.Abstractions;
using Xunit;

namespace Utilities.TSql.UnitTests
{
    public class SqlTransactionShould
    {
        private readonly IFixture _fixture;

        public SqlTransactionShould()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Ctor_Theories_ShouldThrow(string connectionString)
        {
            // Arrange
            var action = () => new SqlTransaction(connectionString);

            // Act && Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("jk;k")]
        [InlineData("hello world")]
        public void Ctor_Theories_ShouldNotThrow(string connectionString)
        {
            // Arrange
            var action = () => new SqlTransaction(connectionString);

            // Act && Assert
            action.Should().NotThrow();

            using var tran = action();
            tran.Should().NotBeNull();
            tran.SqlClientTransaction.Should().BeNull();
        }

        [Fact]
        public void BeginTransaction_ShouldThrow_ObjectDisposedException_WhenIsDisposed()
        {
            // Arrange
            using var tran = new SqlTransaction(_fixture.Create<string>());
            tran.Dispose();

            // Act
            var act = () => tran.BeginTransaction();

            // Assert
            act.Should().Throw<ObjectDisposedException>();
        }

        [Fact]
        public void Commit_ShouldThrow_ObjectDisposedException_WhenIsDisposed()
        {
            // Arrange
            using var tran = new SqlTransaction(_fixture.Create<string>());
            tran.Dispose();

            // Act
            var act = () => tran.Commit();

            // Assert
            act.Should().Throw<ObjectDisposedException>();
        }

        [Fact]
        public void Commit_ShouldThrow_InvalidOperationException_WhenCalledBeforeBeginTransaction()
        {
            // Arrange
            using var tran = new SqlTransaction(_fixture.Create<string>());

            // Act 
            var act = () => tran.Commit();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Commit failed*");
        }

        [Fact]
        public void Rollback_ShouldThrow_ObjectDisposedException_WhenIsDisposed()
        {
            // Arrange
            using var tran = new SqlTransaction(_fixture.Create<string>());
            tran.Dispose();

            // Act
            var act = () => tran.Rollback();

            // Assert
            act.Should().Throw<ObjectDisposedException>();
        }

        [Fact]
        public void Rollback_ShouldThrow_InvalidOperationException_WhenCalledBeforeBeginTransaction()
        {
            // Arrange
            using var tran = new SqlTransaction(_fixture.Create<string>());

            // Act 
            var act = () => tran.Rollback();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Rollback failed*");
        }
    }
}

