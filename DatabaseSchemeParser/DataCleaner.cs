using DatabaseSchemeParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSchemeParser
{
    public class DataCleaner
    {
        private static readonly HashSet<string> validTypes = new HashSet<string>
        {
            "DATABASE",
            "TABLE",
            "COLUMN"
        };

        public IEnumerable<DatabaseSchemeRow> CleanDatabaseSchemeRows(IEnumerable<DatabaseSchemeRow> databaseSchemeRows)
        {
            if (databaseSchemeRows == null)
            {
                throw new ArgumentNullException("Database scheme rows cannot be null.");
            }

            databaseSchemeRows = Standarize(databaseSchemeRows);
            return RemoveInvalidRecords(databaseSchemeRows);
        }

        private IEnumerable<DatabaseSchemeRow> Standarize(IEnumerable<DatabaseSchemeRow> databaseSchemeRows)
        {
            foreach (var databaseSchemeRow in databaseSchemeRows)
            {
                if (databaseSchemeRow != null)
                {
                    databaseSchemeRow.Type = databaseSchemeRow.Type?.Trim().Replace(" ", "").ToUpper();
                    databaseSchemeRow.Name = databaseSchemeRow.Name?.Trim().Replace(" ", "");
                    databaseSchemeRow.Schema = databaseSchemeRow.Schema?.Trim().Replace(" ", "");
                    databaseSchemeRow.ParentName = databaseSchemeRow.ParentName?.Trim().Replace(" ", "");
                    databaseSchemeRow.ParentType = databaseSchemeRow.ParentType?.Trim().Replace(" ", "").ToUpper();
                    databaseSchemeRow.DataType = databaseSchemeRow.DataType?.Trim().Replace(" ", "");
                    databaseSchemeRow.IsNullable = databaseSchemeRow.IsNullable?.Trim().Replace(" ", "");
                }
            }

            return databaseSchemeRows;
        }

        private IEnumerable<DatabaseSchemeRow> RemoveInvalidRecords(IEnumerable<DatabaseSchemeRow> databaseSchemeRows)
        {
            return databaseSchemeRows.Where(x => validTypes.Contains(x?.Type));
        }
    }
}