#region

using Common.ReadOnlyDictionaries.Editor;
using Global.Services.Profiles.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services
{
    [CustomPropertyDrawer(typeof(ProfileLogs))]
    public class ProfileLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}