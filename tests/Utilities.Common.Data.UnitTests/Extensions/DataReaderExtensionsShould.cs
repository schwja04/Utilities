using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utilities.Common.Data.Exceptions;
using Utilities.Common.Data.Extensions;
using Utilities.Common.Testing.Sql;
using Xunit;

namespace Utilities.Common.Data.UnitTests.Extensions
{
    public class DataReaderExtensionsShould
    {
        private readonly IFixture _fixture;

        public DataReaderExtensionsShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void To_WithoutDefault_ShouldTestConditions()
        {
            // Arrange
            Person[] people = CreatePeople(1).ToArray();
            IDataReader reader = TestDataHelper.ToDataReader(people);

            // Act && Assert
            reader.Read().Should().BeTrue();

            // part of contract
            reader.To<int>("age").Should().Be(people[0].Age);
            reader.To<string>("name").Should().Be(people[0].Name);

            var act = () => reader.To<int>("does not exist");
            act.Should().Throw<ColumnNotFoundException>();

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Fact]
        public void To_WithDefault_ShouldTestConditions()
        {
            // Arrange
            var table = new[]
            {
                new
                {
                    NullableInt = (int?)null,
                    StandardInt = 1,
                }
            };

            IDataReader reader = TestDataHelper.ToDataReader(table);

            // Act && Assert
            reader.Read().Should().BeTrue();

            // part of contract
            reader.To<int>("NullableInt", -1).Should().Be(-1);
            reader.To<int>("StandardInt", -1).Should().Be(1);

            var act = () => reader.To<int>("does not exist");
            act.Should().Throw<ColumnNotFoundException>();

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Fact]
        public void ToNullable_WithoutDefault_ShouldTestConditions()
        {
            // Arrange
            var table = new[]
            {
                new
                {
                    NullableInt = (int?)null,
                    StandardInt = 1,
                }
            };

            IDataReader reader = TestDataHelper.ToDataReader(table);

            // Act && Assert
            reader.Read().Should().BeTrue();

            reader.ToNullable<int>("StandardInt").Should().Be(1);
            reader.ToNullable<int>("NullableInt").Should().Be(null);

            var act = () => reader.ToNullable<int>("does not exist");
            act.Should().Throw<ColumnNotFoundException>();

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Fact]
        public void ToNullable_WithDefault_ShouldTestConditions()
        {
            // Arrange
            var table = new[]
            {
                new
                {
                    NullableInt = (int?)null,
                    StandardInt = 1,
                }
            };

            IDataReader reader = TestDataHelper.ToDataReader(table);

            // Act && Assert
            reader.Read().Should().BeTrue();

            reader.ToNullable<int>("StandardInt", -1).Should().Be(1);
            reader.ToNullable<int>("NullableInt", -1).Should().Be(-1);

            var act = () => reader.ToNullable<int>("does not exist", -1);
            act.Should().Throw<ColumnNotFoundException>();

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Theory]
        [InlineData("name", true)]
        [InlineData("age", true)]
        [InlineData("does not exist", false)]
        public void ColumnExists_Theories(string columnName, bool columnExpected)
        {
            // Arrange
            Person[] people = CreatePeople(1).ToArray();
            IDataReader reader = TestDataHelper.ToDataReader(people);

            // Act && Assert
            reader.Read().Should().BeTrue();

            reader.ColumnExists(columnName).Should().Be(columnExpected);

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        private IEnumerable<Person> CreatePeople(int? count)
        {
            if (!count.HasValue) return null;

            return count.Value switch
            {
                int value when value == 0 => Enumerable.Empty<Person>(),
                int value when value > 0 => Create(value),
                _ => throw new ArgumentException($"{nameof(count)} cannot be less than 0.", nameof(count))
            };

            IEnumerable<Person> Create(int count)
            {
                List<Person> people = new(count);

                for (int i = 0; i < count; i++)
                {
                    people.Add(new Person
                    {
                        Name = _fixture.Create<string>(),
                        Age = _fixture.Create<int>()
                    });
                }

                return people;
            }
        }

        internal class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
