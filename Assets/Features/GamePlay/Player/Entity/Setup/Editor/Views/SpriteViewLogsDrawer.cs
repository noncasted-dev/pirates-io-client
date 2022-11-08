using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Views.Sprites.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(SpriteViewLogs))]
    public class SpriteViewLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}