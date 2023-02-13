using System;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [DisallowMultipleComponent]
    public class AimBar : MonoBehaviour, IUpdatable
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

        private IDisposable _aimListener;
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
            _aimListener = Msg.Listen<AimDelayEvent>(OnAim);
        }

        private void OnDisable()
        {
            _aimListener?.Dispose();

            if (_isStarted == false)
                return;

            _updater.Remove(this);
        }

        public void OnUpdate(float delta)
        {
            if (_time < 0)
            {
                _isStarted = false;
                _updater.Remove(this);
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

        private void OnAim(AimDelayEvent data)
        {
            _bar.SetActive(true);
            var rect = _background.rect;
            var width = rect.width;

            _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            _time = data.Delay;
            _max = data.Delay;

            if (_isStarted == true)
                _updater.Remove(this);

            _isStarted = true;
            _updater.Add(this);
        }
    }
}