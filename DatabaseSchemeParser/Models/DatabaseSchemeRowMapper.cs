using CsvHelper.Configuration;

namespace DatabaseSchemeParser.Models
{
    public class DatabaseSchemeRowMapper : ClassMap<DatabaseSchemeRow>
    {
        public DatabaseSchemeRowMapper()
        {
            Map(r => r.Type).Name("Type");
            Map(r => r.Name).Name("Name");
            Map(r => r.Schema).Name("Schema");
            Map(r => r.ParentName).Name("ParentName");
            Map(r => r.ParentType).Name("ParentType");
            Map(r => r.DataType).Name("DataType");
            Map(r => r.IsNullable).Name("IsNullable");
        }
    }
}