using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Utilities.Common.Data.Abstractions;
using Utilities.Common.Data.Extensions;
using Xunit;

namespace Utilities.Common.Testing.Sql.UnitTests
{
    public class TestDataHelperShould
    {
        private readonly IFixture _fixture;

        public TestDataHelperShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void ToDataTable_WithNoProperties_ShouldReturn_OneTable()
        {
            // Arrange
            var table = new[]
            {
                new { },
                new { }
            };

            // Act
            var dataTable = TestDataHelper.ToDataTable(table);

            // Assert
            dataTable.Should().NotBeNull();
            dataTable.Columns.Should().BeEmpty();
            dataTable.Rows.Should().HaveCount(table.Length);
        }

        [Fact]
        public void ToDataSet_WithNoProperties_ShouldReturn_OneTable()
        {
            var table = new[]
            {
                new { },
                new { }
            };

            // Act
            var dataSet = TestDataHelper.ToDataSet(table);

            // Assert
            dataSet.Should().NotBeNull();
            dataSet.Tables.Should().HaveCount(1);
            dataSet.Tables[0].Columns.Should().BeEmpty();
            dataSet.Tables[0].Rows.Should().HaveCount(table.Length);
        }

        [Fact]
        public void ToDataReader_MultipleTypesWithinATable_InvalidOperationException()
        {
            //  Arrange

            var table = new List<object>()
            {
                new FullName
                {
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>()
                },
                new Person
                {
                    Name = _fixture.Create<string>(),
                    Age = _fixture.Create<int>()
                }
            };

            // Act
            Action a = () => TestDataHelper.ToDataReader(table);

            //  Assert
            a.Should().Throw<InvalidOperationException>()
                .WithMessage("All rows within a table must be the same .NET Type.");
        }

        [Fact]
        public void ToDataReader_TwoLists_TwoTables()
        {
            // Arrange
            FullName[] fullNames = CreateFullNames(2).ToArray();

            Person[] people = CreatePeople(2).ToArray();

            // Act
            using IDataReader reader = TestDataHelper.ToDataReader(fullNames, people);

            // Assert
            reader.Should().NotBeNull();

            reader.Read().Should().BeTrue();
            reader.To<string>("FirstName").Should().Be(fullNames[0].FirstName);
            reader.To<string>("LastName").Should().Be(fullNames[0].LastName);
            reader.ToNullable<int>("Modifier").Should().Be(fullNames[0].Modifier);

            reader.Read().Should().BeTrue();
            reader.To<string>("FirstName").Should().Be(fullNames[1].FirstName);
            reader.To<string>("LastName").Should().Be(fullNames[1].LastName);
            reader.ToNullable<int>("Modifier").Should().Be(fullNames[1].Modifier);

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeTrue();

            reader.Read().Should().BeTrue();
            reader.To<string>("Name").Should().Be(people[0].Name);
            reader.To<int>("Age").Should().Be(people[0].Age);

            reader.Read().Should().BeTrue();
            reader.To<string>("Name").Should().Be(people[1].Name);
            reader.To<int>("Age").Should().Be(people[1].Age);

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ToDataReader_WithOnePopulatedListOneNonPopulated_ShouldReturn_TwoTables(bool isSecondTableNull)
        {
            // Arrange
            FullName[] fullNames = CreateFullNames(1).ToArray();

            IEnumerable<Person> people = isSecondTableNull ? CreatePeople(null) : CreatePeople(0);

            // Act
            using IDataReader reader = TestDataHelper.ToDataReader(fullNames, people);

            // Assert
            reader.Should().NotBeNull();

            reader.Read().Should().BeTrue();
            reader.To<string>("FirstName").Should().Be(fullNames[0].FirstName);
            reader.To<string>("LastName").Should().Be(fullNames[0].LastName);
            reader.ToNullable<int>("Modifier").Should().Be(fullNames[0].Modifier);

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeTrue();

            reader.Read().Should().BeFalse();
            reader.NextResult().Should().BeFalse();
        }

        [Fact]
        public async Task ToDataReaderAsync_WithTwoLists_ShouldReturn_TwoTables()
        {
            // Arrange
            var fullNames = new[]
            {
                new FullName
                {
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>(),
                    Modifier = _fixture.Create<int>()
                },
                new FullName
                {
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>(),
                    Modifier = null
                }
            };

            var people = new[] {
                new Person()
                {
                    Name = _fixture.Create<string>(),
                    Age = _fixture.Create<int>()
                },
                new Person()
                {
                    Name = _fixture.Create<string>(),
                    Age = _fixture.Create<int>()
                }
            };

            // Act
            using IDataReaderAsync reader = TestDataHelper.ToDataReaderAsync(fullNames, people);

            // Assert
            reader.Should().NotBeNull();

            (await reader.ReadAsync()).Should().BeTrue();
            reader.To<string>("FirstName").Should().Be(fullNames[0].FirstName);
            reader.To<string>("LastName").Should().Be(fullNames[0].LastName);
            reader.ToNullable<int>("Modifier").Should().Be(fullNames[0].Modifier);

            (await reader.ReadAsync()).Should().BeTrue();
            reader.To<string>("FirstName").Should().Be(fullNames[1].FirstName);
            reader.To<string>("LastName").Should().Be(fullNames[1].LastName);
            reader.ToNullable<int>("Modifier").Should().Be(fullNames[1].Modifier);

            (await reader.ReadAsync()).Should().BeFalse();
            (await reader.NextResultAsync()).Should().BeTrue();

            (await reader.ReadAsync()).Should().BeTrue();
            reader.To<string>("Name").Should().Be(people[0].Name);
            reader.To<int>("Age").Should().Be(people[0].Age);

            (await reader.ReadAsync()).Should().BeTrue();
            reader.To<string>("Name").Should().Be(people[1].Name);
            reader.To<int>("Age").Should().Be(people[1].Age);

            (await reader.ReadAsync()).Should().BeFalse();
            (await reader.NextResultAsync()).Should().BeFalse();
        }

        private IEnumerable<FullName> CreateFullNames(int? count)
        {
            if (!count.HasValue) return null;

            return count.Value switch
            {
                int value when value == 0 => Enumerable.Empty<FullName>(),
                int value when value > 0 => Create(value),
                _ => throw new ArgumentException($"{nameof(count)} cannot be less than 0.", nameof(count))
            };

            IEnumerable<FullName> Create(int count)
            {
                List<FullName> fullNames = new(count);

                for (int i = 0; i < count; i++)
                {
                    fullNames.Add(new FullName
                    {
                        FirstName = _fixture.Create<string>(),
                        LastName = _fixture.Create<string>(),
                        Modifier = _fixture.Create<int>()
                    });
                }

                return fullNames;
            }
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

        internal class FullName
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Modifier { get; set; }
        }

        internal class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}

