using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Utilities.Common.Data.UnitTests
{
    public class DataReaderAsyncShould
    {
        private readonly IFixture _fixture;

        private readonly Mock<IDataReader> _mockDataReader;
        private readonly DataReaderAsync _dataReaderAsync;

        public DataReaderAsyncShould()
        {
            _mockDataReader = new Mock<IDataReader>();

            _dataReaderAsync = new DummyReaderAsync(_mockDataReader.Object);

            _fixture = new Fixture();
        }

        [Fact]
        public void Ctor_ShouldThrowArgumentNullException_WhenReaderIsNull()
        {
            // Arrange && Act
            var act = () => new DummyReaderAsync(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Ctor_ShouldSucceed_WhenReaderIsNotNull()
        {
            // Arrange && Act
            var act = () => new DummyReaderAsync(_mockDataReader.Object);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ThisColumnName_ShouldReturnObject()
        {
            // Arrange
            object expected = 100;

            _mockDataReader.Setup(x => x[It.IsAny<string>()]).Returns(expected);

            // Act
            var actual = _dataReaderAsync["test"];

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ThisIndex_ShouldReturnObject()
        {
            // Arrange
            object expected = "Success";

            _mockDataReader.Setup(x => x[It.IsAny<int>()]).Returns(expected);

            // Act
            var actual = _dataReaderAsync[14];

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Depth_ShouldCall_UnderlyingReader()
        {
            // Arrange
            int expectedDepth = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.Depth).Returns(expectedDepth);

            // Act
            var actual = _dataReaderAsync.Depth;

            // Assert
            actual.Should().Be(expectedDepth);
        }

        [Fact]
        public void FieldCount_ShouldCall_UnderlyingReader()
        {
            // Arrange
            int expectedFieldCount = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.FieldCount).Returns(expectedFieldCount);

            // Act
            var actual = _dataReaderAsync.FieldCount;

            // Assert
            actual.Should().Be(expectedFieldCount);
        }

        [Fact]
        public void IsClosed_ShouldCall_UnderlyingReader()
        {
            // Arrange
            bool expected = _fixture.Create<bool>();

            _mockDataReader.Setup(x => x.IsClosed).Returns(expected);

            // Act
            var actual = _dataReaderAsync.IsClosed;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void RecordsAffected_ShouldCall_UnderlyingReader()
        {
            // Arrange
            int expected = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.RecordsAffected).Returns(expected);

            // Act
            var actual = _dataReaderAsync.RecordsAffected;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Close_ShouldCloseTheReader()
        {
            // Arrange
            bool isClosed = false;

            _mockDataReader.Setup(x => x.IsClosed).Returns(() => isClosed);

            _mockDataReader.Setup(x => x.Close()).Callback(() =>
            {
                isClosed = true;
            });

            // Act && Assert
            _dataReaderAsync.IsClosed.Should().BeFalse();
            _dataReaderAsync.Close();
            _dataReaderAsync.IsClosed.Should().BeTrue();
        }

        [Fact]
        public void GetBoolean_ShouldSuccess()
        {
            // Arrange
            bool expected = _fixture.Create<bool>();
            int index = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetBoolean(It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetBoolean(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetByte_ShouldSuccess()
        {
            // Arrange
            byte expected = _fixture.Create<byte>();
            int index = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetByte(It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetByte(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetBytes_ShouldSuccess()
        {
            // Arrange
            long expected = _fixture.Create<long>();
            int reusableInt = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetBytes(
                It.IsAny<int>(),
                It.IsAny<long>(),
                It.IsAny<byte[]>(),
                It.IsAny<int>(),
                It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetBytes(
                i: reusableInt,
                fieldOffset: _fixture.Create<long>(),
                buffer: _fixture.Create<byte[]>(),
                bufferOffset: reusableInt,
                length: reusableInt);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetChar_ShouldSuccess()
        {
            // Arrange
            char expected = _fixture.Create<char>();
            int index = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetChar(It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetChar(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetChars_ShouldSuccess()
        {
            // Arrange
            long expected = _fixture.Create<long>();
            int reusableInt = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetChars(
                It.IsAny<int>(),
                It.IsAny<long>(),
                It.IsAny<char[]>(),
                It.IsAny<int>(),
                It.IsAny<int>())).Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetChars(
                i: reusableInt,
                fieldOffset: _fixture.Create<long>(),
                buffer: _fixture.Create<char[]>(),
                bufferOffset: reusableInt,
                length: reusableInt);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetData_ShouldCallUnderlyingReader_AndReturnIDataReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var dataReader = new Mock<IDataReader>().Object;

            _mockDataReader.Setup(x => x.GetData(It.IsAny<int>())).Returns(dataReader);

            // Act
            using var actual = _dataReaderAsync.GetData(index);

            // Assert
            actual.Should().Be(dataReader);
        }

        [Fact]
        public void GetDataTypeName_CallsUnderlyingReader_returnsTypeName()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<string>();

            _mockDataReader.Setup(x => x.GetDataTypeName(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetDataTypeName(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetDateTime_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<DateTime>();

            _mockDataReader.Setup(x => x.GetDateTime(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetDateTime(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetDecimal_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<Decimal>();

            _mockDataReader.Setup(x => x.GetDecimal(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetDecimal(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetDouble_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<double>();

            _mockDataReader.Setup(x => x.GetDouble(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetDouble(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetFieldType_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<Type>();

            _mockDataReader.Setup(x => x.GetFieldType(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetFieldType(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetFloat_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<float>();

            _mockDataReader.Setup(x => x.GetFloat(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetFloat(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetGuid_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<Guid>();

            _mockDataReader.Setup(x => x.GetGuid(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetGuid(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetInt16_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<short>();

            _mockDataReader.Setup(x => x.GetInt16(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetInt16(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetInt32_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetInt32(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetInt32(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetInt64_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<long>();

            _mockDataReader.Setup(x => x.GetInt64(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetInt64(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetName_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<string>();

            _mockDataReader.Setup(x => x.GetName(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetName(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetOrdinal_CallsUnderlyingReader()
        {
            // Arrange
            var colName = _fixture.Create<string>();
            var expected = _fixture.Create<int>();

            _mockDataReader.Setup(x => x.GetOrdinal(It.IsAny<string>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetOrdinal(colName);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetSchemaTable_CallsUnderlyingReader()
        {
            // Arrange
            var expected = new DataTable();

            _mockDataReader.Setup(x => x.GetSchemaTable())
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetSchemaTable();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetValue_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<object>();

            _mockDataReader.Setup(x => x.GetValue(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetValue(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetValues_CallsUnderlyingReader()
        {
            // Arrange
            var expected = _fixture.Create<int>();
            var values = _fixture.Create<object[]>();

            _mockDataReader.Setup(x => x.GetValues(It.IsAny<object[]>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.GetValues(values);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void IsDBNull_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<bool>();

            _mockDataReader.Setup(x => x.IsDBNull(It.IsAny<int>()))
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.IsDBNull(index);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void NextResult_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<bool>();

            _mockDataReader.Setup(x => x.NextResult())
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.NextResult();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void Read_CallsUnderlyingReader()
        {
            // Arrange
            var index = _fixture.Create<int>();
            var expected = _fixture.Create<bool>();

            _mockDataReader.Setup(x => x.Read())
                .Returns(expected);

            // Act
            var actual = _dataReaderAsync.Read();

            // Assert
            actual.Should().Be(expected);
        }

        public class DummyReaderAsync : DataReaderAsync
        {
            public DummyReaderAsync(IDataReader dataReader)
                : base(dataReader) { }

            public override Task<bool> IsDBNullAsync(int i) =>
                throw new System.NotImplementedException();

            public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken) =>
                throw new System.NotImplementedException();

            public override Task<bool> NextResultAsync() =>
                throw new System.NotImplementedException();

            public override Task<bool> NextResultAsync(CancellationToken cancellationToken) =>
                throw new System.NotImplementedException();

            public override Task<bool> ReadAsync() =>
                throw new System.NotImplementedException();

            public override Task<bool> ReadAsync(CancellationToken cancellationToken) =>
                throw new System.NotImplementedException();
        }
    }
}

