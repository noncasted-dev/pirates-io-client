using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Views.Animators.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(AnimatorLogs))]
    public class AnimatorLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}