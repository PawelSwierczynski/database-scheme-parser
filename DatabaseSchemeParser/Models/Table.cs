using System.Collections.Generic;

namespace DatabaseSchemeParser.Models
{
    public class Table : IDeserializable
    {
        public IEnumerable<Column> Columns { get; set; }

        public string Deserialize()
        {
            throw new System.NotImplementedException();
        }
    }
}