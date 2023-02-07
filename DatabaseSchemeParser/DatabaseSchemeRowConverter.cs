using DatabaseSchemeParser.Models;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSchemeParser
{
    public class DatabaseSchemeRowConverter
    {
        private const string databaseDataType = "DATABASE";
        private const string tableDataType = "TABLE";
        private const string columnDataType = "COLUMN";

        public IEnumerable<Database> Convert(IEnumerable<DatabaseSchemeRow> databaseSchemeRows)
        {
            var databases = databaseSchemeRows.Where(x => x.Type == databaseDataType).Select(x => new Database
            {
                Name = x.Name,
                Tables = new List<Table>()
            }).ToList();

            var tablesAssignedToDatabases = databaseSchemeRows.Where(x => x.Type == tableDataType && x.ParentType == databaseDataType)
                .GroupBy(x => x.ParentName)
                .Select(group => new
                {
                    group.Key,
                    Items = group.Select(x => new Table
                    {
                        Name = x.Name,
                        Schema = x.Schema,
                        Columns = new List<Column>()
                    }).ToList()
                }).ToList();

            var columnsAssignedToTables = databaseSchemeRows.Where(x => x.Type == columnDataType && x.ParentType == tableDataType)
                .GroupBy(x => x.ParentName)
                .Select(group => new
                {
                    group.Key,
                    Items = group.Select(x => new Column
                    {
                        Name = x.Name,
                        Schema = x.Schema,
                        DataType = x.DataType,
                        IsNullable = x.IsNullable != "0"
                    }).ToList()
                }).ToList();

            var tables = tablesAssignedToDatabases.SelectMany(group => group.Items).ToList();

            foreach (var columnGroup in columnsAssignedToTables)
            {
                var matchedTable = tables.FirstOrDefault(t => t.Name == columnGroup.Key);

                if (matchedTable != null)
                {
                    matchedTable.Columns = columnGroup.Items;
                }
            }

            foreach (var tableGroup in tablesAssignedToDatabases)
            {
                var matchedDatabase = databases.FirstOrDefault(d => d.Name == tableGroup.Key);

                if (matchedDatabase != null)
                {
                    matchedDatabase.Tables = tableGroup.Items;
                }
            }

            return databases;
        }
    }
}