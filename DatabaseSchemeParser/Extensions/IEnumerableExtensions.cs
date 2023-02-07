using DatabaseSchemeParser.Models;
using System;
using System.Collections.Generic;

namespace DatabaseSchemeParser.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string Deserialize(this IEnumerable<Database> databaseSchemes)
        {
            string databaseSchemesDescription = "";

            foreach (var databaseScheme in databaseSchemes)
            {
                databaseSchemesDescription += databaseScheme.Deserialize();
            }

            return databaseSchemesDescription;
        }
    }
}