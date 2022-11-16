#region

using System;
using Common.ReadOnlyDictionaries.Runtime;

#endregion

namespace Global.Services.FilesFlow.Logs
{
    [Serializable]
    public class FilesFlowLogs : ReadOnlyDictionary<FilesFlowLogType, bool>
    {
    }
}