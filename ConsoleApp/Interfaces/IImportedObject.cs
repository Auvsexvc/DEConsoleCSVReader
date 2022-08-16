namespace ConsoleApp
{
    internal interface IImportedObject : IImportedObjectBaseClass
    {
        string DataType { get; set; }
        string IsNullable { get; set; }
        double NumberOfChildren { get; set; }
        string ParentName { get; set; }
        string ParentType { get; set; }
        string Schema { get; set; }
    }
}