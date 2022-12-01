using System;
using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim;
using Global.Services.Updaters.Runtime.Abstract;
using UniRx;
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

        private float _time;
        private float _max;
        private bool _isStarted = false;

        private IDisposable _aimListener;
        
        private IUpdater _updater;

        private void Awake()
        {
            _bar.SetActive(false);
        }

        private void OnEnable()
        {
            _aimListener = MessageBroker.Default.Receive<AimDelayEvent>().Subscribe(OnAim);
        }

        private void OnDisable()
        {
            _aimListener?.Dispose();
            
            if (_isStarted == false)
                return;

            _updater.Remove(this);
        }

        private void OnAim(AimDelayEvent data)
        {
            _bar.SetActive(true);
            var rect = _background.rect;
            var width = rect.width;
            var height = rect.width;
            
            _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);

            _time = data.Delay;
            _max = data.Delay;

            if (_isStarted == true)
                _updater.Remove(this);

            _isStarted = true;
            _updater.Add(this);
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
    }
}