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
        [SerializeField] private LevelCameraLogSettings _levelCamera;
        [SerializeField] private ProjectilesLogSettings _projectiles;
        [SerializeField] private TransitionScreenLogSettings _transitionScreen;
    }
}