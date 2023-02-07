namespace DatabaseSchemeParser
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using DatabaseSchemeParser.Models;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    public class DatabaseSchemeReader
    {
        public IEnumerable<Database> ParseDatabaseSchemes(Stream databaseSchemeStream)
        {
            IEnumerable<DatabaseSchemeRow> databaseSchemeRows = RetrieveDatabaseSchemeRows(databaseSchemeStream);

            var dataCleaner = new DataCleaner();

            databaseSchemeRows = dataCleaner.CleanDatabaseSchemeRows(databaseSchemeRows);



            throw new System.NotImplementedException();

            /*for (int i = 0; i < importedLines.Count; i++)
            {
                var importedLine = importedLines[i];

                if (importedLine.Length == 0)
                {
                    continue;
                }

                var values = importedLine.Split(';');
                var importedObject = new ImportedObject();
                importedObject.Type = values[0];
                importedObject.Name = values[1];
                importedObject.Schema = values[2];
                importedObject.ParentName = values[3];
                importedObject.ParentType = values[4];
                importedObject.DataType = values[5];

                if (values.Count() >= 7)
                {
                    importedObject.IsNullable = values[6];
                }

                ((List<ImportedObject>)ImportedObjects).Add(importedObject);
            }

            // clear and correct imported data
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.Type = importedObject.Type?.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                importedObject.Name = importedObject.Name?.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.Schema = importedObject.Schema?.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentName = importedObject.ParentName?.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentType = importedObject.ParentType?.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
            }

            // assign number of children
            for (int i = 0; i < ImportedObjects.Count(); i++)
            {
                var importedObject = ImportedObjects.ToArray()[i];
                foreach (var impObj in ImportedObjects)
                {
                    if (impObj.ParentType == importedObject.Type)
                    {
                        if (impObj.ParentName == importedObject.Name)
                        {
                            importedObject.NumberOfChildren = 1 + importedObject.NumberOfChildren;
                        }
                    }
                }
            }

            return ImportedObjects;*/
        }

        private IEnumerable<DatabaseSchemeRow> RetrieveDatabaseSchemeRows(Stream databaseSchemeStream)
        {
            var csvReaderConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,
                Delimiter = ";"
            };

            using (var streamReader = new StreamReader(databaseSchemeStream))
            {
                using (var csvReader = new CsvReader(streamReader, csvReaderConfiguration))
                {
                    csvReader.Context.RegisterClassMap<DatabaseSchemeRowMapper>();

                    return csvReader.GetRecords<DatabaseSchemeRow>().ToList();
                }
            }
        }
    }
}