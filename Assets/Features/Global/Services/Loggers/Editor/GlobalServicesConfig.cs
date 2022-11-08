using Common.EditableScriptableObjects.Attributes;
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
using Global.Services.ResourcesCleaners.Runtime;
using Global.Services.ScenesFlow.Runtime;
using Global.Services.Updaters.Runtime;
using UnityEngine;

namespace Global.Services.Loggers.Editor
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "Services",
        menuName = GlobalAssetsPaths.Config + "Services", order = 1)]
    public class GlobalServicesConfig : ScriptableObject
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
    }
}