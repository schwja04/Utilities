using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common.Testing.Sql.Data;
using Xunit;

namespace Utilities.Common.Testing.Sql.UnitTests.Data
{
    public class TestDataReaderAsyncShould
    {
        private readonly TestDataReaderAsync _readerAsync;

        private readonly Mock<IDataReader> _mockReader;
        private readonly IFixture _fixture;

        public TestDataReaderAsyncShould()
        {
            _fixture = new Fixture();

            _mockReader = new Mock<IDataReader>();
            _readerAsync = new TestDataReaderAsync(_mockReader.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task IsDBNullAsync_Theories(bool returnValue)
        {
            // Arrange
            var index = _fixture.Create<int>();

            _mockReader.Setup(x => x.IsDBNull(It.IsAny<int>())).Returns(returnValue);

            // Act
            var actual = await _readerAsync.IsDBNullAsync(index);

            // Assert
            actual.Should().Be(returnValue);
            _mockReader.Verify(x => x.IsDBNull(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void IsDBNullAsyncWithCancellationRequested_ShouldThrow_TaskCanceledException()
        {
            // Arrange
            var index = _fixture.Create<int>();

            // Act
            var act = async () => await _readerAsync.IsDBNullAsync(index, new CancellationToken(canceled: true));

            // Assert
            act.Should().ThrowAsync<TaskCanceledException>();
            _mockReader.Verify(x => x.IsDBNull(It.IsAny<int>()), Times.Never());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task IsDBNullAsync_WithCancellationTokenNone_Theories(bool returnValue)
        {
            // Arrange
            var index = _fixture.Create<int>();

            _mockReader.Setup(x => x.IsDBNull(It.IsAny<int>())).Returns(returnValue);

            // Act
            var actual = await _readerAsync.IsDBNullAsync(index, CancellationToken.None);

            // Assert
            actual.Should().Be(returnValue);
            _mockReader.Verify(x => x.IsDBNull(It.IsAny<int>()), Times.Once());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task NextResultAsync_Theories(bool returnValue)
        {
            // Arrange
            _mockReader.Setup(x => x.NextResult()).Returns(returnValue);

            // Act
            var actual = await _readerAsync.NextResultAsync();

            // Assert
            actual.Should().Be(returnValue);
            _mockReader.Verify(x => x.NextResult(), Times.Once());
        }

        [Fact]
        public void NextResultAsyncWithCancellationRequested_ShouldThrow_TaskCanceledException()
        {
            // Arrange && Act
            var act = async () => await _readerAsync.NextResultAsync(new CancellationToken(canceled: true));

            // Assert
            act.Should().ThrowAsync<TaskCanceledException>();
            _mockReader.Verify(x => x.NextResult(), Times.Never());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task NextResultAsync_WithCancellationTokenNone_Theories(bool returnValue)
        {
            // Arrange
            _mockReader.Setup(x => x.NextResult()).Returns(returnValue);

            // Act
            var actual = await _readerAsync.NextResultAsync(CancellationToken.None);

            // Assert
            actual.Should().Be(returnValue);

            _mockReader.Verify(x => x.NextResult(), Times.Once());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReadAsync_Theories(bool returnValue)
        {
            // Arrange
            _mockReader.Setup(x => x.Read()).Returns(returnValue);

            // Act
            var actual = await _readerAsync.ReadAsync();

            // Assert
            actual.Should().Be(returnValue);
            _mockReader.Verify(x => x.Read(), Times.Once());
        }

        [Fact]
        public void ReadAsyncWithCancellationRequested_ShouldThrow_TaskCanceledException()
        {
            // Arrange && Act
            var act = async () => await _readerAsync.ReadAsync(new CancellationToken(canceled: true));

            // Assert
            act.Should().ThrowAsync<TaskCanceledException>();
            _mockReader.Verify(x => x.Read(), Times.Never());
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReadAsync_WithCancellationTokenNone_Theories(bool returnValue)
        {
            // Arrange
            _mockReader.Setup(x => x.Read()).Returns(returnValue);

            // Act
            var actual = await _readerAsync.ReadAsync(CancellationToken.None);

            // Assert
            actual.Should().Be(returnValue);

            _mockReader.Verify(x => x.Read(), Times.Once());
        }
    }
}

