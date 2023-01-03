using System;
using Common.Local.ComposedSceneConfig;
using Cysharp.Threading.Tasks;
using GamePlay.Level.Config.Runtime;
using Global.GameLoops.Flow;
using Global.GameLoops.Logs;
using Global.Services.Common.Abstract;
using Global.Services.Common.Scope;
using Global.Services.CurrentCameras.Runtime;
using Global.Services.CurrentSceneHandlers.Runtime;
using Global.Services.GlobalCameras.Runtime;
using Global.Services.LoadingScreens.Runtime;
using Global.Services.ScenesFlow.Runtime.Abstract;
using Menu.Bootstrap;
using Menu.Services.MenuLoop.Runtime;
using UniRx;
using UnityEngine;
using VContainer;

namespace Global.GameLoops.Runtime
{
    public class GameLoop : MonoBehaviour, IMenuLoader, ILevelLoader, IGlobalAwakeListener
    {
        [Inject]
        private void Construct(
            GlobalScope scope,
            ISceneLoader loader,
            ILoadingScreen loadingScreen,
            IGlobalCamera globalCamera,
            ICurrentSceneHandler currentSceneHandler,
            ICurrentCamera currentCamera,
            GameLoopLogger logger)
        {
            _logger = logger;
            _scope = scope;
            _loader = loader;
            _loadingScreen = loadingScreen;
            _globalCamera = globalCamera;
            _currentSceneHandler = currentSceneHandler;
            _currentCamera = currentCamera;
        }

        [SerializeField] private LevelAsset _level;

        [SerializeField] private MenuAsset _menu;

        private ICurrentCamera _currentCamera;
        private ICurrentSceneHandler _currentSceneHandler;
        private IGlobalCamera _globalCamera;

        private ISceneLoader _loader;
        private ILoadingScreen _loadingScreen;
        private GameLoopLogger _logger;

        private IDisposable _playFromMenuEvent;

        private GlobalScope _scope;

        private void OnDestroy()
        {
            _playFromMenuEvent?.Dispose();
        }

        public void OnAwake()
        {
            _playFromMenuEvent = MessageBroker.Default.Receive<PlayFromMenuEvent>().Subscribe(OnPlayFromMenu);
        }

        public void LoadLevel()
        {
            _logger.OnLoadLevel();

            LoadScene(_level).Forget();
        }

        public void LoadMenu()
        {
            _logger.OnLoadMenu();

            LoadScene(_menu).Forget();
        }

        public void Begin()
        {
            _logger.OnBegin();

            LoadMenu();
        }

        private void OnPlayFromMenu(PlayFromMenuEvent data)
        {
            LoadLevel();
        }

        private async UniTaskVoid LoadScene(ComposedSceneAsset asset)
        {
            _globalCamera.Enable();
            _currentCamera.SetCamera(_globalCamera.Camera);

            _loadingScreen.Show();

            var unload = _currentSceneHandler.Unload();
            var result = await asset.Load(_scope, _loader);

            await unload;
            await _currentSceneHandler.FinalizeUnloading();

            _currentSceneHandler.OnLoaded(result);
            _globalCamera.Disable();
            _loadingScreen.Hide();

            result.OnLoaded();
        }
    }
}