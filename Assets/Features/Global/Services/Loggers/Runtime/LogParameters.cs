using System;
using UnityEngine;

namespace Global.Services.Loggers.Runtime
{
    [Serializable]
    public class LogParameters
    {
        [field: SerializeField] private string _header;
        [field: SerializeField] private bool _isHeaderColoredColored;
        [field: SerializeField] private bool _isMessageColored;
        [field: SerializeField] private LogColor Color;

        public string Header => _header;
        public bool IsHeaderColored => _isHeaderColoredColored;
        public bool IsMessageColored => _isMessageColored;

        public string GetColor()
        {
            return Color switch
            {
                LogColor.Red => "red",
                LogColor.Green => "green",
                LogColor.Blue => "blue",
                LogColor.Yellow => "yellow",
                LogColor.Purple => "purple",
                LogColor.Orange => "orange",
                LogColor.Black => "black",
                LogColor.White => "white",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public bool ContainsHeader()
        {
            return !string.IsNullOrEmpty(_header);
        }
    }
}