using ConsoleApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Extensions
{
    public static class ImportedObjectExtension
    {
        public static ImportedObject MakeUp(this ImportedObject importedObject)
        {
            importedObject.Type = importedObject.Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToTitleCase();
            importedObject.Name = importedObject.Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            importedObject.Schema = importedObject.Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            importedObject.ParentName = importedObject.ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
            importedObject.ParentType = importedObject.ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToTitleCase();

            return importedObject;
        }

        public static IEnumerable<ImportedObject> AssignChildrenCount(this IEnumerable<ImportedObject> importedObjects)
        {
            var obj = importedObjects.Where(i => i.Type != nameof(ImportedObjectType.Column))
                .GroupBy(k => (k, importedObjects.Count(x => x.ParentType == k.Type && x.ParentName == k.Name)))
                .Select(g => (g.Key.k, g.Key.Item2)).ToList();

            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].k.NumberOfChildren = obj[i].Item2;
            }

            return importedObjects.Where(i => i.Type == nameof(ImportedObjectType.Column)).Concat(obj.Select(x => x.k));
        }
    }
}