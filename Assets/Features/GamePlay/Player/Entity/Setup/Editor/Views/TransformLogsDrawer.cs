using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Views.Transforms.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(TransformLogs))]
    public class TransformLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}