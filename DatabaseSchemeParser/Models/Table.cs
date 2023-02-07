using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSchemeParser.Models
{
    public class Table : IDeserializable
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public IEnumerable<Column> Columns { get; set; }

        public string Deserialize()
        {
            string tableDescription = $"\tTable '{Schema}.{Name}' ({Columns.Count()} columns){Environment.NewLine}";

            foreach (var column in Columns)
            {
                tableDescription += column.Deserialize();
            }

            return tableDescription;
        }
    }
}