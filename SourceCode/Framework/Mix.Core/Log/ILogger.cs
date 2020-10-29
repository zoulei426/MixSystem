namespace Mix.Core.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Error<T>(string message, T propertyValue);
        void Infomation(string message);
        void Warning(string message);
    }
}