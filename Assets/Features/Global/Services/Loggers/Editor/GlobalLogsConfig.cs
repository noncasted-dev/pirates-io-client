using Common.EditableScriptableObjects.Attributes;
using GamePlay.Common.SceneObjects.Logs;
using Global.Common;
using Global.GameLoops.Logs;
using Global.Services.ApplicationProxies.Logs;
using Global.Services.AssetsFlow.Logs;
using Global.Services.CameraUtilities.Logs;
using Global.Services.CurrentCameras.Logs;
using Global.Services.CurrentSceneHandlers.Logs;
using Global.Services.FilesFlow.Logs;
using Global.Services.GlobalCameras.Logs;
using Global.Services.InputViews.Logs;
using Global.Services.LoadingScreens.Logs;
using Global.Services.ResourcesCleaners.Logs;
using Global.Services.ScenesFlow.Logs;
using Global.Services.Updaters.Logs;
using UnityEngine;

namespace Global.Services.Loggers.Editor
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "Logs",
        menuName = GlobalAssetsPaths.Config + "Logs", order = 1)]
    public class GlobalLogsConfig : ScriptableObject
    {
        [SerializeField] [EditableObject] private ApplicationProxyLogSettings _applicationProxy;
        [SerializeField] [EditableObject] private AssetsFlowLogSettings _assetsFlow;
        [SerializeField] [EditableObject] private CameraUtilsLogSettings _cameraUtils;
        [SerializeField] [EditableObject] private CurrentCameraLogSettings _currentCamera;
        [SerializeField] [EditableObject] private CurrentSceneHandlerLogSettings _currentSceneHandler;
        [SerializeField] [EditableObject] private FilesFlowLogSettings _filesFlow;
        [SerializeField] [EditableObject] private GlobalCameraLogSettings _globalCamera;
        [SerializeField] [EditableObject] private InputViewLogSettings _inputView;
        [SerializeField] [EditableObject] private LoadingScreenLogSettings _loadingScreen;
        [SerializeField] [EditableObject] private ResourcesCleanerLogSettings _resourcesCleaner;
        [SerializeField] [EditableObject] private ScenesFlowLogSettings _scenesFlow;
        [SerializeField] [EditableObject] private UpdaterLogSettings _updater;
        [SerializeField] [EditableObject] private SceneObjectLogSettings _sceneObject;
        [SerializeField] [EditableObject] private GameLoopLogSettings _gameLoop;
    }
}