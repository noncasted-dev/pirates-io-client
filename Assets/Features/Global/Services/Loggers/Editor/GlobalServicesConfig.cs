using GamePlay.Common.SceneObjects.Global;
using Global.Common;
using Global.Services.ApplicationProxies.Runtime;
using Global.Services.AssetsFlow.Runtime;
using Global.Services.CameraUtilities.Runtime;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.CurrentSceneHandlers.Runtime;
using Global.Services.DebugConsoles.Runtime;
using Global.Services.FilesFlow.Runtime;
using Global.Services.GlobalCameras.Runtime;
using Global.Services.InputViews.Runtime;
using Global.Services.LoadingScreens.Runtime;
using Global.Services.Loggers.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Network.Bootstrap;
using Global.Services.ResourcesCleaners.Runtime;
using Global.Services.ScenesFlow.Runtime;
using Global.Services.UiStateMachines.Runtime;
using Global.Services.Updaters.Runtime;
using UnityEngine;

namespace Global.Services.Loggers.Editor
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "Services",
        menuName = GlobalAssetsPaths.Config + "Services")]
    public class GlobalServicesConfig : ScriptableObject
    {
        [SerializeField] private ApplicationProxyAsset _applicationProxy;
        [SerializeField] private AssetsFlowAsset _assetsFlow;
        [SerializeField] private CameraUtilsAsset _cameraUtils;
        [SerializeField] private CurrentCameraAsset _currentCamera;
        [SerializeField] private CurrentSceneHandlerAsset _currentSceneHandler;
        [SerializeField] private DebugConsoleAsset _debugConsole;
        [SerializeField] private FilesFlowAsset _filesFlow;
        [SerializeField] private GlobalCameraAsset _globalCamera;
        [SerializeField] private InputViewAsset _inputView;
        [SerializeField] private LoadingScreenAsset _loadingScreen;
        [SerializeField] private LoggerAsset _logger;
        [SerializeField] private ResourcesCleanerAsset _resourcesCleaner;
        [SerializeField] private ScenesFlowAsset _scenesFlow;
        [SerializeField] private UiStateMachineAsset _uiStateMachine;
        [SerializeField] private UpdaterAsset _updater;
        [SerializeField] private SceneObjectsAsset _sceneObject;
        [SerializeField] private NetworkAsset _network;
        [SerializeField] private MessageBrokerAsset _messageBroker;
    }
}