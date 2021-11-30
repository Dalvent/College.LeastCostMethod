using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeastCostMethod
{
    public static class Program
    {
        private const string ResultsFileName = "results.txt";
        private static ILogger Logger { get; } = new Logger();
        
        private static decimal[] ReadDecimals()
        {
            try
            { 
                string userInput = Console.ReadLine(); 
                return userInput
                    .Split(' ')
                    .Select(item => Convert.ToDecimal(item.Trim(), CultureInfo.InvariantCulture))
                    .ToArray();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Not valid input format!");
            }
        }

        private static decimal[,] ReadMatrix(int xCount, int yCount)
        {
            decimal[,] results = new decimal[xCount, yCount];
            for (int y = 0; y < yCount; y++)
            {
                var row = ReadDecimals();
                for (int x = 0; x < xCount; x++)
                {
                    results[x, y] = row[x];
                }
            }

            return results;
        }
        
        static async Task Main(string[] args)
        {
            decimal[] headerX;
            decimal[] headerY;
            decimal[,] costsMatrix;
            try
            {
                Console.WriteLine("Write Header X: ");
                headerX = ReadDecimals();
                Console.WriteLine("Write Header Y: ");
                headerY = ReadDecimals();
                Console.WriteLine("Write Matrix of costs");
                costsMatrix = ReadMatrix(headerX.Length, headerY.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Not valid table!");
                return;
            }
            
            CostsTable costsTable = new(headerX, headerY, costsMatrix);
            LeastCostMoveIterator leastCostMoveIterator = new();

            Console.WriteLine(costsTable.LeastCostTableToString());
            Console.ReadKey();
            Console.Clear();
            
            while (leastCostMoveIterator.NextMove(costsTable))
            {
                Logger.Log(costsTable.LeastCostTableToString());
                Console.ReadKey();
                Console.Clear();
            }

            Logger.Log(new string('=', 40));
            Logger.Log(costsTable.LeastCostTableToString());
            Logger.Log($"Result sum: {costsTable.ResultSum}");
            
            var task = WriteInResultFileAsync(Logger.History);
            Console.WriteLine("Saving results in file...");
            await task;
            Console.WriteLine($"Results saved in {ResultsFileName}");
            
            
            Console.ReadKey();
        }

        private static async Task WriteInResultFileAsync(string value)
        {
            using var fileStream = new FileStream(ResultsFileName, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = Encoding.UTF8.GetBytes(value);

            await fileStream.WriteAsync(bytes);
        }
    }
}