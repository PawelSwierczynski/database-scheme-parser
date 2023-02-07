using DatabaseSchemeParser.Extensions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace DatabaseSchemeParser.Tests.Integration
{
    public class DatabaseSchemeReaderTests
    {
        private const string expectedSampleDatabaseSchemeDataFilepath = "SampleData/expectedSampleDatabaseSchemeData.txt";
        private const string sampleDatabaseSchemeDataFilepath = "SampleData/sampleDatabaseSchemeData.csv";

        [Test]
        public void ParseDatabaseSchemes_DatabaseSchemeStreamIsNull_ThrowsArgumentNullException()
        {
            Stream databaseSchemeStream = null;
            var databaseSchemeReader = new DatabaseSchemeReader();

            Action parsingDatabaseSchemes = () => databaseSchemeReader.ParseDatabaseSchemes(databaseSchemeStream);

            parsingDatabaseSchemes.Should().ThrowExactly<ArgumentNullException>();
        }

        [Test]
        public void ParseDatabaseSchemes_DatabaseSchemeStreamIsEmpty_ReturnsEmptyList()
        {
            var databaseSchemeStream = new MemoryStream();
            var databaseSchemeReader = new DatabaseSchemeReader();

            var actual = databaseSchemeReader.ParseDatabaseSchemes(databaseSchemeStream);

            actual.Should().BeEmpty();
        }

        [Test]
        public void ParseDatabaseSchemes_DatabaseSchemeStreamIsNotEmpty_ReturnsCorrectList()
        {
            var expected = File.ReadAllText(expectedSampleDatabaseSchemeDataFilepath);
            var databaseSchemeStream = new FileStream(sampleDatabaseSchemeDataFilepath, FileMode.Open, FileAccess.Read);
            var databaseSchemeReader = new DatabaseSchemeReader();

            var parsedDatabaseSchemes = databaseSchemeReader.ParseDatabaseSchemes(databaseSchemeStream);
            var actual = parsedDatabaseSchemes.Deserialize();

            actual.Should().BeEquivalentTo(expected);
        }
    }
}