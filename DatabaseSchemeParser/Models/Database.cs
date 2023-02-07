using System.Collections.Generic;

namespace DatabaseSchemeParser.Models
{
    public class Database : IDeserializable
    {
        public IEnumerable<Table> Tables { get; set; }

        public string Deserialize()
        {
            throw new System.NotImplementedException();
        }
    }
}