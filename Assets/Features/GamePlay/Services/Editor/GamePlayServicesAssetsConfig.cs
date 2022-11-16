using Common.EditableScriptableObjects.Attributes;
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
        [SerializeField] [EditableObject] private LevelCameraAsset _levelCamera;
        [SerializeField] [EditableObject] private LevelEnvironmentAsset _levelEnvironment;
        [SerializeField] [EditableObject] private LevelLoopAsset _levelLoop;
        [SerializeField] [EditableObject] private PlayerFactoryAsset _playerFactory;
        [SerializeField] [EditableObject] private ProjectilesAsset _projectiles;
        [SerializeField] [EditableObject] private TransitionScreenAsset _transitionScreen;
        [SerializeField] [EditableObject] private VfxPoolAsset _vfxPool;
    }
}