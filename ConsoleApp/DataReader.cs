namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataReader
    {
        private readonly IEnumerable<IImportedObject> _importedObjects;

        public DataReader(IEnumerable<string> data)
        {
            var importedObjects = ImportLinesToDataReader(data);
            var correctedObjects = ClearAndCorrectImportedData(importedObjects);

            _importedObjects = AssignNumberOfChildren(correctedObjects);
        }

        public void Print()
        {
            foreach (var database in _importedObjects.Where(database => string.Equals(database.Type, "DATABASE", StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                foreach (var table in _importedObjects)
                {
                    if (string.Equals(table.ParentType, database.Type, StringComparison.CurrentCultureIgnoreCase)
                        && string.Equals(table.ParentName, database.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                        foreach (var column in _importedObjects)
                        {
                            if (string.Equals(column.ParentType, table.Type, StringComparison.CurrentCultureIgnoreCase)
                                && string.Equals(column.ParentName, table.Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        private IEnumerable<IImportedObject> ImportLinesToDataReader(IEnumerable<string> importedLines)
        {
            List<IImportedObject> retList = new List<IImportedObject>();

            for (int i = 0; i < importedLines.Count(); i++)
            {
                var importedLine = importedLines.ElementAt(i);
                if (importedLine.Length == 0)
                {
                    continue;
                }

                var values = importedLine.Split(';');

                if (values.Length != 7)
                {
                    continue;
                }
                var importedObject = new ImportedObject
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };

                retList.Add(importedObject);
            }

            return retList;
        }

        private IEnumerable<IImportedObject> ClearAndCorrectImportedData(IEnumerable<IImportedObject> importedObjects)
        {
            foreach (var importedObject in importedObjects)
            {
                importedObject.Type = importedObject.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                importedObject.Name = importedObject.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.Schema = importedObject.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentName = importedObject.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
                importedObject.ParentType = importedObject.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            }

            return importedObjects;
        }

        private IEnumerable<IImportedObject> AssignNumberOfChildren(IEnumerable<IImportedObject> importedObjects)
        {
            for (int i = 0; i < importedObjects.Count(); i++)
            {
                var importedObject = importedObjects.ElementAtOrDefault(i);
                foreach (var impObj in importedObjects)
                {
                    if (string.Equals(impObj.ParentType, importedObject.Type, StringComparison.CurrentCultureIgnoreCase)
                        && string.Equals(impObj.ParentName, importedObject.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        importedObject.NumberOfChildren++;
                    }
                }
            }

            return importedObjects;
        }
    }
}