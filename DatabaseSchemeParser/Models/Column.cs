using System;

namespace DatabaseSchemeParser.Models
{
    public class Column : IDeserializable
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string DataType { get; set; }
        public bool IsNullable { get; set; }

        public string Deserialize()
        {
            return $"\t\tColumn '{Name}' with {DataType} data type {(IsNullable ? "accepts nulls" : "with no nulls")}{Environment.NewLine}";
        }
    }
}