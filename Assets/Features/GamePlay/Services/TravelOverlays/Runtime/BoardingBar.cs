using System;
using GamePlay.Player.Entity.Components.Boardings.Local.Events;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class BoardingBar : MonoBehaviour, IUpdatable
    {
        [Inject]
        private void Construct(IUpdater updater)
        {
            _updater = updater;
        }

        [SerializeField] private float _minLength = 10f;
        [SerializeField] private GameObject _bar;
        [SerializeField] private RectTransform _fill;
        [SerializeField] private RectTransform _background;

        private IDisposable _preparationListener;
        private IDisposable _cancelListener;
        private IDisposable _startListener;
        
        private bool _isStarted = false;
        private float _max;

        private float _time;

        private IUpdater _updater;

        private void Awake()
        {
            _bar.SetActive(false);
        }

        private void OnEnable()
        {
            _preparationListener = Msg.Listen<BoardingPreparationEvent>(OnPrepare);
            _cancelListener = Msg.Listen<BoardingCancelEvent>(OnCanceled);
            _startListener = Msg.Listen<BoardingStartEvent>(OnStarted);
        }

        private void OnDisable()
        {
            _preparationListener?.Dispose();
            _cancelListener?.Dispose();
            _startListener?.Dispose();

            Stop();
        }

        private void Stop()
        {
            if (_isStarted == false)
                return;

            _updater.Remove(this);
            _isStarted = false;
        }

        public void OnUpdate(float delta)
        {
            if (_time < 0)
            {
                Stop();
                
                _bar.SetActive(false);
                _time = 0;
                return;
            }

            _time -= delta;

            var progress = _time / _max;

            var rect = _background.rect;
            var width = rect.width;
            var length = Mathf.Lerp(0, width, progress);

            if (length < _minLength)
                length = _minLength;

            _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        }

        private void OnPrepare(BoardingPreparationEvent data)
        {
            _bar.SetActive(true);
            var rect = _background.rect;
            var width = rect.width;

            _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            _time = data.Time;
            _max = data.Time;

            Stop();

            _isStarted = true;
            _updater.Add(this);
        }

        private void OnCanceled(BoardingCancelEvent data)
        {
            _bar.SetActive(false);

            Stop();
        }

        private void OnStarted(BoardingStartEvent data)
        {
            _bar.SetActive(false);

            Stop();
        }
    }
}