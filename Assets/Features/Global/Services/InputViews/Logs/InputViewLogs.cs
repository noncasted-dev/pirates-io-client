using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.InputViews.Logs
{
    [Serializable]
    public class InputViewLogs : ReadOnlyDictionary<InputViewLogType, bool>
    {
    }
}