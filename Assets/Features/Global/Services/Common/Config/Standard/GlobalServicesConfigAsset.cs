using Common.EditableScriptableObjects.Attributes;
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
using Global.Services.UiStateMachines.Runtime;
using Global.Services.Updaters.Runtime;
using UnityEngine;

namespace Global.Services.Common.Config.Standard
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.BootstrapPrefix + "Services",
        menuName = GlobalAssetsPaths.BootstrapConfig)]
    public class GlobalServicesConfigAsset : GlobalServicesConfig
    {
        [SerializeField] [EditableObject] private ApplicationProxyAsset _applicationProxy;
        [SerializeField] [EditableObject] private AssetsFlowAsset _assetsFlow;
        [SerializeField] [EditableObject] private CameraUtilsAsset _cameraUtils;
        [SerializeField] [EditableObject] private CurrentCameraAsset _currentCamera;
        [SerializeField] [EditableObject] private CurrentSceneHandlerAsset _currentSceneHandler;
        [SerializeField] [EditableObject] private FilesFlowAsset _filesFlow;
        [SerializeField] [EditableObject] private GlobalCameraAsset _globalCamera;
        [SerializeField] [EditableObject] private InputViewAsset _inputView;
        [SerializeField] [EditableObject] private LoadingScreenAsset _loadingScreen;
        [SerializeField] [EditableObject] private LoggerAsset _logger;
        [SerializeField] [EditableObject] private ResourcesCleanerAsset _resourcesCleaner;
        [SerializeField] [EditableObject] private ScenesFlowAsset _scenesFlow;
        [SerializeField] [EditableObject] private UpdaterAsset _updater;
        [SerializeField] [EditableObject] private SceneObjectsAsset _sceneObject;
        [SerializeField] [EditableObject] private DebugConsoleAsset _debugConsole;
        [SerializeField] [EditableObject] private NetworkAsset _network;
        [SerializeField] [EditableObject] private ProfileAsset _profile;
        [SerializeField] [EditableObject] private PersistentInventoryAsset _persistentInventory;
        [SerializeField] [EditableObject] private ItemFactoryAsset _itemFactory;
        [SerializeField] [EditableObject] private UiStateMachineAsset _uiStateMachine;

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
                _uiStateMachine
            };
        }
    }
}