using UnityEngine;

namespace Global.Services.Loggers.Runtime
{
    public class Logger : MonoBehaviour, ILogger
    {
        private readonly MessageBuilder _messageBuilder = new();

        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Log(string header, string message)
        {
            Debug.Log($"[{header}]: {message}");
        }

        public void Log(string message, LogParameters parameters)
        {
            Debug.Log(_messageBuilder.Build(message, parameters));
        }

        public void Warning(string warning)
        {
            Debug.Log(warning);
        }

        public void Error(string error)
        {
            Debug.LogError(error);
        }

        public void Error(string error, LogParameters parameters)
        {
            Debug.LogError(_messageBuilder.Build(error, parameters));
        }
    }
}