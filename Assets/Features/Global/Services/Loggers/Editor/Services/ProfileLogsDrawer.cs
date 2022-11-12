using Common.ReadOnlyDictionaries.Editor;
using UnityEditor;

namespace Features.Global.Services.Profiles.Logs
{
    [CustomPropertyDrawer(typeof(ProfileLogs))]
    public class ProfileLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}