using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.UiStateMachines.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [DisallowMultipleComponent]
    public class TransitionScreen : MonoBehaviour, ITransitionScreen, IUiState
    {
        [Inject]
        private void Construct(
            IUpdater updater,
            IUiStateMachine uiStateMachine,
            UiConstraints constraints,
            TransitionScreenLogger logger,
            TransitionScreenConfigAsset config)
        {
            _constraints = constraints;
            _uiStateMachine = uiStateMachine;
            _logger = logger;
            _config = config;
            _updater = updater;
        }

        [SerializeField] private Image _background;

        [SerializeField] private GameObject _canvas;
        private TransitionScreenConfigAsset _config;

        private Fade _current;
        private TransitionScreenLogger _logger;

        private IUpdater _updater;
        private IUiStateMachine _uiStateMachine;
        private UiConstraints _constraints;
        
        public UiConstraints Constraints => _constraints;
        public string Name => "TransitionScreen";
        
        public void Recover()
        {
            ToPlayerRespawn();
        }

        public void Exit()
        {
            _canvas.SetActive(false);
        }

        public void ToPlayerRespawn()
        {
            _uiStateMachine.EnterAsSingle(this);
            
            _canvas.SetActive(true);
            _background.SetAlphaOne();

            _logger.OnToPlayerSpawn();
        }

        public void ToPlayerDeath()
        {
            _uiStateMachine.EnterAsSingle(this);
            
            _canvas.SetActive(true);
            _background.SetAlphaZero();

            _logger.OnToPlayerDeath();
        }

        public async UniTask FadeIn()
        {
            if (_current != null)
            {
                _current.Dispose();
                _logger.OnFadeOverlap();
            }

            _current = new Fade(_updater, _logger, _background, _config.FadeSpeed);
            await _current.FadeIn(this.GetCancellationTokenOnDestroy());

            _current.Dispose();
            _current = null;

            _canvas.SetActive(false);
            
            _uiStateMachine.Exit(this);
        }

        public async UniTask FadeOut()
        {
            _current?.Dispose();

            _current = new Fade(_updater, _logger, _background, _config.FadeSpeed);
            await _current.FadeOut(this.GetCancellationTokenOnDestroy());

            _current.Dispose();
            _current = null;

            _canvas.SetActive(false);
            
            _uiStateMachine.Exit(this);
        }
    }
}