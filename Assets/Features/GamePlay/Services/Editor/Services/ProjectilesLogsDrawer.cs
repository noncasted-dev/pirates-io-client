using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.Projectiles.Logs;
using UnityEditor;

namespace GamePlay.Services.Editor.Services
{
    [CustomPropertyDrawer(typeof(ProjectilesLogs))]
    public class ProjectilesLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}