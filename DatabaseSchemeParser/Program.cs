using DatabaseSchemeParser.Extensions;
using System;
using System.IO;

namespace DatabaseSchemeParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var databaseSchemeReader = new DatabaseSchemeReader();
            var parsedDatabaseSchemes = databaseSchemeReader.ParseDatabaseSchemes(new FileStream("data.csv", FileMode.Open));

            Console.WriteLine(parsedDatabaseSchemes.Deserialize());
        }
    }
}