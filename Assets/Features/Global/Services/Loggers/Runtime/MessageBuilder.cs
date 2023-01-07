using System.Text;

namespace Global.Services.Loggers.Runtime
{
    public class MessageBuilder
    {
        public string Build(string message, LogParameters parameters)
        {
            var stringBuilder = new StringBuilder();

            var headers = parameters.Headers;

            foreach (var header in headers)
                if (header.IsColored == true)
                    stringBuilder.Append(ApplyColor($"[{header.Name}]: ", header.Color));
                else
                    stringBuilder.Append($"[{header.Name}]: ");

            if (parameters.IsMessageColored == true)
                stringBuilder.Append(ApplyColor(message, parameters.Color));
            else
                stringBuilder.Append(message);

            return stringBuilder.ToString();
        }

        private string ApplyColor(string log, string color)
        {
            return $"<color=#{color}>{log}</color>";
        }
    }
}