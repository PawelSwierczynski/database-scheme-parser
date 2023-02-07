using CsvHelper.Configuration;

namespace DatabaseSchemeParser.Models
{
    public class DatabaseSchemeRowMapper : ClassMap<DatabaseSchemeRow>
    {
        public DatabaseSchemeRowMapper()
        {
            Map(x => x.Type).Name("Type");
            Map(x => x.Name).Name("Name");
            Map(x => x.Schema).Name("Schema");
            Map(x => x.ParentName).Name("ParentName");
            Map(x => x.ParentType).Name("ParentType");
            Map(x => x.DataType).Name("DataType");
            Map(x => x.IsNullable).Name("IsNullable");
        }
    }
}