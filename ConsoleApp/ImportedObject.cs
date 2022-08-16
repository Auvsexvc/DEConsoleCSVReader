namespace ConsoleApp
{
    public class ImportedObject : ImportedObjectBaseClass, IImportedObject
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public double NumberOfChildren { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
    }
}