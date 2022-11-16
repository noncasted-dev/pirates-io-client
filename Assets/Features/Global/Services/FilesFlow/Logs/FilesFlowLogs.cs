using System;
using Common.ReadOnlyDictionaries.Runtime;

namespace Global.Services.FilesFlow.Logs
{
    [Serializable]
    public class FilesFlowLogs : ReadOnlyDictionary<FilesFlowLogType, bool>
    {
    }
}