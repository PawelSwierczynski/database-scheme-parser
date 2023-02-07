using DatabaseSchemeParser.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DatabaseSchemeParser.Tests
{
    public class DataCleanerTests
    {
        [Test]
        public void CleanDatabaseSchemeRows_DataIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<DatabaseSchemeRow> databaseSchemeRows = null;
            var dataCleaner = new DataCleaner();

            Action cleaningData = () => dataCleaner.CleanDatabaseSchemeRows(databaseSchemeRows);

            cleaningData.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestCaseSource(nameof(RetrieveCorrectTestCases))]
        public void CleanDatabaseSchemeRows_DataIsValid_ReturnsCleanedData(IEnumerable<DatabaseSchemeRow> databaseSchemeRows, IEnumerable<DatabaseSchemeRow> expected)
        {
            var dataCleaner = new DataCleaner();

            var actual = dataCleaner.CleanDatabaseSchemeRows(databaseSchemeRows);

            actual.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<TestCaseData> RetrieveCorrectTestCases()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(new List<DatabaseSchemeRow>(), new List<DatabaseSchemeRow>()).SetArgDisplayNames("emptyList"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    null
                }, new List<DatabaseSchemeRow>()).SetArgDisplayNames("nullElement"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow()
                }, new List<DatabaseSchemeRow>()).SetArgDisplayNames("emptyElement"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "Table",
                        ParentType = "Database"
                    }
                },
                new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "TABLE",
                        ParentType = "DATABASE"
                    }
                }).SetArgDisplayNames("pascalCase"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "table",
                        ParentType = "database"
                    }
                },
                new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "TABLE",
                        ParentType = "DATABASE"
                    }
                }).SetArgDisplayNames("camelCase"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "TaBlE",
                        ParentType = "DatAbaSE"
                    }
                },
                new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Type = "TABLE",
                        ParentType = "DATABASE"
                    }
                }).SetArgDisplayNames("mixedCase"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Name = "  abc",
                        Type = " TaB lE",
                        ParentName = "test    ",
                        ParentType = "DatA baSE ",
                        Schema = " sc he ma    ",
                        DataType = "dT   ",
                        IsNullable = " 1 "
                    }
                },
                new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Name = "abc",
                        Type = "TABLE",
                        ParentName = "test",
                        ParentType = "DATABASE",
                        Schema = "schema",
                        DataType = "dT",
                        IsNullable = "1"
                    }
                }).SetArgDisplayNames("unstandarized")
            };
        }
    }
}