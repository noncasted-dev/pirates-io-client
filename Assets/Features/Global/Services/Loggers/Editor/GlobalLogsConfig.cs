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
using Global.Services.MessageBrokers.Logs;
using Global.Services.Network.Connection.Logs;
using Global.Services.Network.Instantiators.Logs;
using Global.Services.Network.Session.Join.Logs;
using Global.Services.Network.Session.Leave.Logs;
using Global.Services.ResourcesCleaners.Logs;
using Global.Services.ScenesFlow.Logs;
using Global.Services.UiStateMachines.Logs;
using Global.Services.Updaters.Logs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Services.Loggers.Editor
{
    [CreateAssetMenu(fileName = GlobalAssetsPaths.ConfigPrefix + "Logs",
        menuName = GlobalAssetsPaths.Config + "Logs", order = 1)]
    public class GlobalLogsConfig : ScriptableObject
    {
        [SerializeField] private ApplicationProxyLogSettings _applicationProxy;
        [SerializeField] private AssetsFlowLogSettings _assetsFlow;
        [SerializeField] private CameraUtilsLogSettings _cameraUtils;
        [SerializeField] private CurrentCameraLogSettings _currentCamera;
        [SerializeField] private CurrentSceneHandlerLogSettings _currentSceneHandler;
        [SerializeField] private FilesFlowLogSettings _filesFlow;
        [SerializeField] private GlobalCameraLogSettings _globalCamera;
        [SerializeField] private InputViewLogSettings _inputView;
        [SerializeField] private LoadingScreenLogSettings _loadingScreen;
        [SerializeField] private ResourcesCleanerLogSettings _resourcesCleaner;
        [SerializeField] private ScenesFlowLogSettings _scenesFlow;
        [SerializeField] private UiStateMachineLogSettings _uiStateMachine;
        [SerializeField] private UpdaterLogSettings _updater;
        [SerializeField] private SceneObjectLogSettings _sceneObject;
        [SerializeField] private GameLoopLogSettings _gameLoop;
        [SerializeField] private MessageBrokerLogSettings _messageBroker;

        [SerializeField] [FoldoutGroup("Network")]
        private NetworkConnectorLogSettings _networkConnector;

        [SerializeField] [FoldoutGroup("Network")]
        private NetworkInstantiatorLogSettings _networkInstantiator;

        [SerializeField] [FoldoutGroup("Network")]
        private NetworkSessionJoinLogSettings _networkSessionJoin;

        [SerializeField] [FoldoutGroup("Network")]
        private NetworkSessionLeaveLogSettings _networkSessionLeave;
    }
}