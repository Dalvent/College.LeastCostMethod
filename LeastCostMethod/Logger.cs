using System;
using System.Text;

namespace LeastCostMethod
{
    public class Logger : ILogger
    {
        private StringBuilder _historyBuilder = new();
        
        public void Log(string value)
        {
            _historyBuilder.AppendLine(value);
            
            Console.WriteLine(value);
        }

        public string History => _historyBuilder.ToString();
    }
}