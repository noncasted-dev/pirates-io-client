#region

using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.PlayerSpawn.Factory.Logs;
using UnityEditor;

#endregion

namespace GamePlay.Services.Editor.Services
{
    [CustomPropertyDrawer(typeof(PlayerFactoryLogs))]
    public class PlayerFactoryLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}