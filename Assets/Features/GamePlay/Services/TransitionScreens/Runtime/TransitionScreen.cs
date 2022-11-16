#region

using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

#endregion

namespace GamePlay.Services.TransitionScreens.Runtime
{
    [DisallowMultipleComponent]
    public class TransitionScreen : MonoBehaviour, ITransitionScreen
    {
        [Inject]
        private void Construct(
            IUpdater updater,
            TransitionScreenLogger logger,
            TransitionScreenConfigAsset config)
        {
            _logger = logger;
            _config = config;
            _updater = updater;
        }

        [SerializeField] private GameObject _canvas;
        [SerializeField] private Image _background;
        private TransitionScreenConfigAsset _config;

        private Fade _current;
        private TransitionScreenLogger _logger;

        private IUpdater _updater;

        public void ToPlayerRespawn()
        {
            _canvas.SetActive(true);
            _background.SetAlphaOne();

            _logger.OnToPlayerSpawn();
        }

        public void ToPlayerDeath()
        {
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
        }

        public async UniTask FadeOut()
        {
            _current?.Dispose();

            _current = new Fade(_updater, _logger, _background, _config.FadeSpeed);
            await _current.FadeOut(this.GetCancellationTokenOnDestroy());

            _current.Dispose();
            _current = null;

            _canvas.SetActive(false);
        }
    }
}