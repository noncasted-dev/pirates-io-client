using GamePlay.Common.SceneObjects.Global;
using Global.Common;
using Global.Services.ApplicationProxies.Runtime;
using Global.Services.AssetsFlow.Runtime;
using Global.Services.CameraUtilities.Runtime;
using Global.Services.Common.Abstract;
using Global.Services.Common.Config.Abstract;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.CurrentSceneHandlers.Runtime;
using Global.Services.DebugConsoles.Runtime;
using Global.Services.FilesFlow.Runtime;
using Global.Services.GlobalCameras.Runtime;
using Global.Services.InputViews.Runtime;
using Global.Services.ItemFactories.Runtime;
using Global.Services.LoadingScreens.Runtime;
using Global.Services.Loggers.Runtime;
using Global.Services.Network.Bootstrap;
using Global.Services.PersistentInventories.Runtime;
using Global.Services.Profiles.Storage;
using Global.Services.ResourcesCleaners.Runtime;
using Global.Services.ScenesFlow.Runtime;
using Global.Services.Sounds.Runtime;
using Global.Services.UiStateMachines.Runtime;
using Global.Services.Updaters.Runtime;
using UnityEngine;

namespace Global.Services.Common.Config.Standard
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.BootstrapPrefix + "Services",
        menuName = GlobalAssetsPaths.BootstrapConfig)]
    public class GlobalServicesConfigAsset : GlobalServicesConfig
    {
        [SerializeField] private ApplicationProxyAsset _applicationProxy;
        [SerializeField] private AssetsFlowAsset _assetsFlow;
        [SerializeField] private CameraUtilsAsset _cameraUtils;
        [SerializeField] private CurrentCameraAsset _currentCamera;
        [SerializeField] private CurrentSceneHandlerAsset _currentSceneHandler;
        [SerializeField] private FilesFlowAsset _filesFlow;
        [SerializeField] private GlobalCameraAsset _globalCamera;
        [SerializeField] private InputViewAsset _inputView;
        [SerializeField] private LoadingScreenAsset _loadingScreen;
        [SerializeField] private LoggerAsset _logger;
        [SerializeField] private ResourcesCleanerAsset _resourcesCleaner;
        [SerializeField] private ScenesFlowAsset _scenesFlow;
        [SerializeField] private UpdaterAsset _updater;
        [SerializeField] private SceneObjectsAsset _sceneObject;
        [SerializeField] private DebugConsoleAsset _debugConsole;
        [SerializeField] private NetworkAsset _network;
        [SerializeField] private ProfileAsset _profile;
        [SerializeField] private PersistentInventoryAsset _persistentInventory;
        [SerializeField] private ItemFactoryAsset _itemFactory;
        [SerializeField] private UiStateMachineAsset _uiStateMachine;
        [SerializeField] private SoundsPlayerAsset _soundsPlayer;

        public override GlobalServiceAsset[] GetAssets()
        {
            return new GlobalServiceAsset[]
            {
                _applicationProxy,
                _assetsFlow,
                _cameraUtils,
                _currentCamera,
                _currentSceneHandler,
                _filesFlow,
                _globalCamera,
                _inputView,
                _loadingScreen,
                _logger,
                _resourcesCleaner,
                _scenesFlow,
                _updater,
                _sceneObject,
                _debugConsole,
                _network,
                _profile,
                _persistentInventory,
                _itemFactory,
                _uiStateMachine,
                _soundsPlayer,
            };
        }
    }
}