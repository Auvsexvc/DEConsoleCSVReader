using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    public class DataMiner
    {
        public IEnumerable<string> ExtractFromFile(string fileToImport)
        {
            List<string> retList = new List<string>();
            using (var streamReader = new StreamReader(fileToImport))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    retList.Add(line);
                }
            }

            return retList;
        }
    }
}