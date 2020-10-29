namespace Mix.Core.Log
{
    public interface ILogger
    {
        void Error(string message);
        void Infomation(string message);
        void Warning(string message);
    }
}