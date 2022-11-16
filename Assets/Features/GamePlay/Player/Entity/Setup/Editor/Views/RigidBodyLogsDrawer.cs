using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Player.Entity.Views.RigidBodies.Logs;
using UnityEditor;

namespace GamePlay.Player.Entity.Setup.Editor.Views
{
    [CustomPropertyDrawer(typeof(RigidBodyLogs))]
    public class RigidBodyLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}