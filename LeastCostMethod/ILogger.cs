namespace LeastCostMethod
{
    public interface ILogger
    {
        void Log(string value);
        string History { get; }
    }
}