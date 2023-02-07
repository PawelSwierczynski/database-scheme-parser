using DatabaseSchemeParser.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace DatabaseSchemeParser.Tests
{
    public class DatabaseSchemeConverterTests
    {
        [TestCaseSource(nameof(RetrieveTestCases))]
        public void Convert_ListIsValid_ShouldReturnConvertedList(IEnumerable<DatabaseSchemeRow> databaseSchemeRows, IEnumerable<Database> expected)
        {

        }

        private static IEnumerable<TestCaseData> RetrieveTestCases()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Name = "abc",
                        Type = "DATABASE"
                    }
                },
                new List<Database>
                {
                    new Database
                    {
                        Name = "abc"
                    }
                }).SetArgDisplayNames("singleDatabase"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Name = "abc",
                        Type = "DATABASE"
                    },
                    new DatabaseSchemeRow
                    {
                        Name = "def",
                        Type = "TABLE",
                        Schema = "schema",
                        ParentName = "abc",
                        ParentType = "DATABASE"
                    }
                },
                new List<Database>
                {
                    new Database
                    {
                        Name = "abc",
                        Tables = new List<Table>
                        {
                            new Table
                            {
                                Name = "def",
                                Schema = "schema"
                            }
                        }
                    }
                }).SetArgDisplayNames("databaseWithTable"),

                new TestCaseData(new List<DatabaseSchemeRow>
                {
                    new DatabaseSchemeRow
                    {
                        Name = "abc",
                        Type = "DATABASE"
                    },
                    new DatabaseSchemeRow
                    {
                        Name = "def",
                        Type = "TABLE",
                        Schema = "schema",
                        ParentName = "abc",
                        ParentType = "DATABASE"
                    },
                    new DatabaseSchemeRow
                    {
                        Name = "ghi",
                        Type = "COLUMN",
                        Schema = "schema",
                        ParentName = "def",
                        ParentType = "TABLE",
                        DataType = "int",
                        IsNullable = "1"
                    }
                },
                new List<Database>
                {
                    new Database
                    {
                        Name = "abc",
                        Tables = new List<Table>
                        {
                            new Table
                            {
                                Name = "def",
                                Schema = "schema",
                                Columns = new List<Column>
                                {
                                    new Column
                                    {
                                        Name = "ghi",
                                        Schema = "schema",
                                        DataType = "int",
                                        IsNullable = true
                                    }
                                }
                            }
                        }
                    }
                }).SetArgDisplayNames("databaseWithTableAndColumn")
            };
        }
    }
}