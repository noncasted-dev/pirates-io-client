#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Common.SceneObjects.Logs;
using UnityEditor;

#endregion

namespace Global.Services.Loggers.Editor.Services.Scenes
{
    [CustomPropertyDrawer(typeof(SceneObjectLogs))]
    public class SceneObjectLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}