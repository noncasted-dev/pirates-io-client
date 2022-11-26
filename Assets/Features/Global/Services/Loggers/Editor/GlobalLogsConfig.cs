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
using Global.Services.Network.Connection.Logs;
using Global.Services.Network.Instantiators.Logs;
using Global.Services.Network.Session.Join.Logs;
using Global.Services.Network.Session.Leave.Logs;
using Global.Services.Profiles.Logs;
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
        [Space(30)] [Header("System")] 
        [SerializeField] 
        private ApplicationProxyLogSettings _applicationProxy;
        [Space(30)] [Header("Data")] [SerializeField] 
        private AssetsFlowLogSettings _assetsFlow;
        [Header("Camera")] [SerializeField] 
        private CameraUtilsLogSettings _cameraUtils;
        [SerializeField]  private CurrentCameraLogSettings _currentCamera;
        [SerializeField]  private CurrentSceneHandlerLogSettings _currentSceneHandler;
        [SerializeField]  private FilesFlowLogSettings _filesFlow;
        [SerializeField]  private GameLoopLogSettings _gameLoop;
        [SerializeField]  private GlobalCameraLogSettings _globalCamera;
        [Space(30)] [Header("GamePlay")] [SerializeField] 
        private InputViewLogSettings _inputView;
        [Space(30)] [Header("UI")] [SerializeField] 
        private LoadingScreenLogSettings _loadingScreen;
        [Space(30)] [Header("Network")] [SerializeField] 
        private NetworkConnectorLogSettings _networkConnector;
        [SerializeField]  private NetworkInstantiatorLogSettings _networkInstantiator;
        [SerializeField]  private NetworkSessionJoinLogSettings _networkJoin;
        [SerializeField]  private NetworkSessionLeaveLogSettings _networkLeave;
        [SerializeField]  private ProfileLogSettings _profile;
        [SerializeField]  private ResourcesCleanerLogSettings _resourcesCleaner;
        [SerializeField]  private SceneObjectLogSettings _sceneObject;
        [Space(30)] [Header("Scenes")] [SerializeField] 
        private ScenesFlowLogSettings _scenesFlow;
        [SerializeField]  private UpdaterLogSettings _updater;
    }
}