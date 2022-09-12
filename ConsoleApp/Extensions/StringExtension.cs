using ConsoleApp.Enums;
using System;
using System.Linq;

namespace ConsoleApp.Extensions
{
    public static class StringExtension
    {
        public static ImportedObject ToImportedObject(this string line)
        {
            var propsCount = new ImportedObject().GetType().GetProperties().Length;
            var values = line.Split(';');

            if (values.Length < propsCount)
            {
                Array.Resize(ref values, propsCount);
            }

            if (Enum.GetNames(typeof(ImportedObjectType)).Any(e => string.Equals(values[0], e, StringComparison.OrdinalIgnoreCase)))
            {
                return new ImportedObject()
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6],
                };
            }

            return null;
        }

        public static string ToTitleCase(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? str : char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}