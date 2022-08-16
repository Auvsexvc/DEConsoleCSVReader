# Dataedo

Original code Review:

1. Multiple types in one file. - Solution : one type per file
2. Base class not defined as abstract. - Solution : defined as abstract
3. Properties general mess. Solution : unified declaration style and formatting, fixed property hiding by derived type.
4. DataReader type dependent upon concretions. Solution : Extracted interfaces from ImportedObjectBaseClass and ImportedObject what made DataReader dependent upon abstractions (interfaces)
5. DataReader responsibility too large. Originally the type was responsible for importing data from file via StreamReader, injecting data into objects, manipulating (correcting, assigning) data in objects and finally printing... If it werent enough it was all done by one method. Solution : extracted "importing from file" functionality into diffrent type - DataMiner with one metod (ExtractFromFile()), making it ready to be extended for further import types.  In DataReader more methods were extracted: one for importing string lines into DataReader type objects,  second method responsible for correcting imported data, third method responsible for assigning number of children, and fourth method - for printing purpose. 
6. Some buisness logic needed to be fixed.
7. Fixed binary expressions
8. Fixed formatting
