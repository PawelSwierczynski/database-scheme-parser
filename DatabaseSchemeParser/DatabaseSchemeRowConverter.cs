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
        private const string notNullableColumnSyntax = "0";

        public IEnumerable<Database> Convert(IEnumerable<DatabaseSchemeRow> databaseSchemeRows)
        {
            var databases = databaseSchemeRows.Where(d => d.Type == databaseDataType).Select(d => new Database
            {
                Name = d.Name,
                Tables = new List<Table>()
            }).ToList();

            var tablesAssignedToDatabases = databaseSchemeRows.Where(t => t.Type == tableDataType && t.ParentType == databaseDataType)
                .GroupBy(t => t.ParentName)
                .Select(group => new
                {
                    group.Key,
                    Items = group.Select(t => new Table
                    {
                        Name = t.Name,
                        Schema = t.Schema,
                        Columns = new List<Column>()
                    }).ToList()
                }).ToList();

            var columnsAssignedToTables = databaseSchemeRows.Where(c => c.Type == columnDataType && c.ParentType == tableDataType)
                .GroupBy(c => c.ParentName)
                .Select(group => new
                {
                    group.Key,
                    Items = group.Select(c => new Column
                    {
                        Name = c.Name,
                        Schema = c.Schema,
                        DataType = c.DataType,
                        IsNullable = c.IsNullable != notNullableColumnSyntax
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