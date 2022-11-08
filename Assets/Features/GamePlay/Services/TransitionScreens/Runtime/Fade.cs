using System.Threading;
using Common.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Services.TransitionScreens.Logs;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.TransitionScreens.Runtime
{
    public class Fade : IUpdatable
    {
        public Fade(
            IUpdater updater,
            TransitionScreenLogger logger,
            Image image,
            float speed)
        {
            _updater = updater;
            _logger = logger;
            _image = image;
            _speed = speed;
            _startAlpha = _image.color.a;

            _completion = new UniTaskCompletionSource<bool>();
        }

        private readonly UniTaskCompletionSource<bool> _completion;
        private readonly Image _image;
        private readonly TransitionScreenLogger _logger;
        private readonly float _speed;
        private readonly float _startAlpha;

        private readonly IUpdater _updater;
        private CancellationToken _cancellation;
        private float _progress;

        private float _target;

        public void OnUpdate(float delta)
        {
            if (_cancellation.IsCancellationRequested == true)
            {
                _completion.TrySetCanceled(_cancellation);
                return;
            }

            _progress += delta * _speed;
            var alpha = Mathf.Lerp(_startAlpha, _target, _progress);
            _image.SetAlpha(alpha);

            _logger.OnFadeProcess(_progress, _image.color.a, _speed);

            if (_progress < 1f)
                return;

            _image.SetAlpha(_target);
            _completion.TrySetResult(true);
        }

        public async UniTask FadeIn(CancellationToken token)
        {
            _logger.OnFadeInStart();

            _cancellation = token;

            _target = 1f;

            _updater.Add(this);

            await _completion.Task;

            _updater.Remove(this);
        }

        public async UniTask FadeOut(CancellationToken token)
        {
            _logger.OnFadeOutStart();

            _cancellation = token;

            _target = 0f;

            _updater.Add(this);

            await _completion.Task;

            _updater.Remove(this);
        }

        public void Dispose()
        {
            _logger.OnFadeCanceled();
            _updater.Remove(this);
            _completion.TrySetCanceled(_cancellation);
        }
    }
}