using Cysharp.Threading.Tasks;
using GamePlay.Common.SceneObjects.Runtime;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Services.LevelCameras.Runtime;
using GamePlay.Services.LevelLoops.Logs;
using GamePlay.Services.PlayerSpawn.Factory.Runtime;
using GamePlay.Services.TransitionScreens.Runtime;
using Global.Services.CurrentCameras.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.LevelLoops.Runtime
{
    [DisallowMultipleComponent]
    public class LevelLoop : MonoBehaviour, ILocalLoadCallbackListener
    {
        [Inject]
        private void Construct(
            ISceneObjectsHandler sceneObjects,
            IPlayerFactory playerFactory,
            ICurrentCamera currentCamera,
            ILevelCamera levelCamera,
            ITransitionScreen transitionScreen,
            LevelLoopLogger logger)
        {
            _logger = logger;
            _transitionScreen = transitionScreen;
            _sceneObjects = sceneObjects;
            _playerFactory = playerFactory;
            _currentCamera = currentCamera;
            _levelCamera = levelCamera;
        }

        private ICurrentCamera _currentCamera;
        private ILevelCamera _levelCamera;
        private LevelLoopLogger _logger;
        private IPlayerRoot _player;
        private IPlayerFactory _playerFactory;
        private ISceneObjectsHandler _sceneObjects;
        private ITransitionScreen _transitionScreen;

        public void OnLoaded()
        {
            _sceneObjects.InvokeFullStartup();

            _currentCamera.SetCamera(_levelCamera.Camera);
            _transitionScreen.ToPlayerRespawn();

            _logger.OnLoaded();

            Begin().Forget();
        }

        private async UniTask Begin()
        {
            _logger.OnPlayerSpawn();

            _player = await _playerFactory.Create();

            _levelCamera.Teleport(_player.Transform.position);
            _levelCamera.StartFollow(_player.Transform);
            await _transitionScreen.FadeOut();

            _player.Respawn();
        }
    }
}