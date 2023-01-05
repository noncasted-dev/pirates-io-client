using GamePlay.Common.Paths;
using Global.Services.Loggers.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Services.LevelLoops.Logs
{
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.LogsPrefix + "LevelLoop",
        menuName = GamePlayAssetsPaths.LevelLoop + "Logs")]
    public class LevelLoopLogSettings : LogSettings<LevelLoopLogs, LevelLoopLogType>
    {
        [SerializeField] [Indent] private LogParameters _logParameters;

        public LogParameters LogParameters => _logParameters;
    }
}