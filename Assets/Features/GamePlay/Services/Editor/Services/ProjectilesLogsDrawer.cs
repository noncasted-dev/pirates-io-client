#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.Projectiles.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Services.Editor.Services
{
    [CustomPropertyDrawer(typeof(ProjectilesLogs))]
    public class ProjectilesLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}