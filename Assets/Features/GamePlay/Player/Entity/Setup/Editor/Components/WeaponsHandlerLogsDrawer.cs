using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Weapons.Handler.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Components
{
    [CustomPropertyDrawer(typeof(WeaponsHandlerLogs))]
    public class WeaponsHandlerLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}