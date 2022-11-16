using Common.EditableScriptableObjects.Attributes;
using GamePlay.Common.Paths;
using GamePlay.Services.LevelCameras.Logs;
using GamePlay.Services.Projectiles.Logs;
using GamePlay.Services.TransitionScreens.Logs;
using UnityEngine;

namespace GamePlay.Services.Editor
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "Logs",
        menuName = GamePlayAssetsPaths.Config + "Logs")]
    public class GamePlayServicesLogsConfig : ScriptableObject
    {
        [SerializeField] [EditableObject] private LevelCameraLogSettings _levelCamera;
        [SerializeField] [EditableObject] private ProjectilesLogSettings _projectiles;
        [SerializeField] [EditableObject] private TransitionScreenLogSettings _transitionScreen;
    }
}