using DatabaseSchemeParser.Extensions;
using System;
using System.IO;

namespace DatabaseSchemeParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var databaseSchemeReader = new DatabaseSchemeReader();
            var parsedDatabaseSchemes = databaseSchemeReader.ParseDatabaseSchemes(new FileStream("data.csv", FileMode.Open));

            Console.WriteLine(parsedDatabaseSchemes.Deserialize());
        }
    }
}