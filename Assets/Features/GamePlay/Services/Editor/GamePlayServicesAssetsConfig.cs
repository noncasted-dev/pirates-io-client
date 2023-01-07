using GamePlay.Common.Paths;
using GamePlay.Level.Environment.Bootstrap;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Runtime;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.Projectiles.Bootstrap;
using GamePlay.Services.TransitionScreens.Runtime;
using GamePlay.Services.VFX.Pool.Provider;
using UnityEngine;

namespace GamePlay.Services.Editor
{
    [CreateAssetMenu(fileName = GamePlayAssetsPaths.ConfigPrefix + "ServicesAssets",
        menuName = GamePlayAssetsPaths.Config + "ServicesAssets")]
    public class GamePlayServicesAssetsConfig : ScriptableObject
    {
        [SerializeField] private LevelCameraAsset _levelCamera;
        [SerializeField] private LevelEnvironmentAsset _levelEnvironment;
        [SerializeField] private LevelLoopAsset _levelLoop;
        [SerializeField] private PlayerFactoryAsset _playerFactory;
        [SerializeField] private ProjectilesAsset _projectiles;
        [SerializeField] private TransitionScreenAsset _transitionScreen;
        [SerializeField] private VfxPoolAsset _vfxPool;
    }
}