using System;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class HealthBar : MonoBehaviour
    {
        private IDisposable _healthListener;

        private RectTransform _transform;
        private float _maxLength;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
            _maxLength = _transform.sizeDelta.x;
        }

        private void OnEnable()
        {
            _healthListener = MessageBroker.Default.Receive<HealthChangedEvent>().Subscribe(OnHealthChanged);
        }

        private void OnDisable()
        {
            _healthListener?.Dispose();
        }

        private void OnHealthChanged(HealthChangedEvent data)
        {
            var progress = data.Current / (float)data.Max;
            var size = _maxLength * progress;

            _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        }
    }
}