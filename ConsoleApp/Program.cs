namespace ConsoleApp
{
    public static class Program
    {
        private static void Main()
        {
            var miner = new DataMiner();
            var data = miner.ExtractFromFile("data.csv");

            var reader = new DataReader(data);
            reader.Print();
        }
    }
}