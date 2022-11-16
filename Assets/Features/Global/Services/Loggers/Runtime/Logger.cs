#region

using System.Text;
using UnityEngine;

#endregion

namespace Global.Services.Loggers.Runtime
{
    public class Logger : MonoBehaviour, ILogger
    {
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
            Debug.Log(BuildLog(message, parameters));
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
            Debug.LogError(BuildLog(error, parameters));
        }

        private string BuildLog(string log, LogParameters parameters)
        {
            var stringBuilder = new StringBuilder();

            if (parameters.ContainsHeader() == true)
            {
                if (parameters.IsHeaderColored == true)
                    stringBuilder.Append(ApplyColor($"[{parameters.Header}]: ", parameters.GetColor()));
                else
                    stringBuilder.Append($"[{parameters.Header}]: ");
            }

            if (parameters.IsMessageColored == true)
                stringBuilder.Append(ApplyColor(log, parameters.GetColor()));
            else
                stringBuilder.Append(log);

            return stringBuilder.ToString();
        }

        private string ApplyColor(string log, string color)
        {
            return $"<color={color}>{log}</color>";
        }
    }
}