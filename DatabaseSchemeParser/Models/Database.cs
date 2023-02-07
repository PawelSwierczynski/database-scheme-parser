using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSchemeParser.Models
{
    public class Database : IDeserializable
    {
        public string Name { get; set; }
        public IEnumerable<Table> Tables { get; set; }

        public string Deserialize()
        {
            string databaseDescription = $"Database '{Name}' ({Tables.Count()} tables){Environment.NewLine}";

            foreach (var table in Tables)
            {
                databaseDescription += table.Deserialize();
            }

            return databaseDescription;
        }
    }
}