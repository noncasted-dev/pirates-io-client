using Common.ReadOnlyDictionaries.Editor;
using GamePlay.Services.LevelCameras.Logs;
using UnityEditor;

namespace GamePlay.Services.Editor.Services
{
    [CustomPropertyDrawer(typeof(LevelCameraLogs))]
    public class LevelCameraLogsDrawer : ReadonlyDictionaryPropertyDrawer
    {
        protected override bool IsCollapsed => false;
    }
}