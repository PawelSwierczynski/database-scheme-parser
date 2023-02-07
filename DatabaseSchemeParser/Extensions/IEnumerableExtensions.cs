using DatabaseSchemeParser.Models;
using System.Collections.Generic;

namespace DatabaseSchemeParser.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string Deserialize(this IEnumerable<Database> importedObjects)
        {
            string importedObjectsDescription = "";

            /*foreach (var database in importedObjects)
            {
                if (database.Type == "DATABASE")
                {
                    importedObjectsDescription += $"Database '{database.Name}' ({database.NumberOfChildren} tables)\r\n";

                    // print all database's tables
                    foreach (var table in importedObjects)
                    {
                        if (table.ParentType?.ToUpper() == database.Type)
                        {
                            if (table.ParentName == database.Name)
                            {
                                importedObjectsDescription += $"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)\r\n";

                                // print all table's columns
                                foreach (var column in importedObjects)
                                {
                                    if (column.ParentType?.ToUpper() == table.Type)
                                    {
                                        if (column.ParentName == table.Name)
                                        {
                                            importedObjectsDescription += $"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}\r\n";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

            return importedObjectsDescription;
        }
    }
}