using Global.Services.Network.Instantiators.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Views.Transforms.Runtime
{
    [DisallowMultipleComponent]
    public class NetworkTransform : RagonBehaviour, INetworkTransform
    {
        [SerializeField] private float _interpolationBreakDistance = 1f;
        [SerializeField] private float _interpolationSpeed = 1f;

        private readonly RagonVector3 _position = new(Vector3.zero, RagonAxis.XY);

        private Vector2 _lastReplicated;
        private Vector2 _localPosition;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetPosition(Vector2 position)
        {
            _localPosition = position;
            _transform.position = position;
        }

        public override void OnCreatedEntity()
        {
            var payload = Entity.GetSpawnPayload<NetworkPayload>();
            _position.Value = payload.Position;
        }

        public override void OnEntityTick()
        {
            _position.Value = _localPosition;
        }

        public override void OnProxyTick()
        {
            var target = _position.Value;
            var current = _transform.position;
            
            var distance = Vector3.Distance(target, current);
            
            if (distance > _interpolationBreakDistance)
            {
                _transform.position = target;
                return;
            }
            
            _transform.position =
                Vector3.Lerp(_transform.position, current, Time.deltaTime * _interpolationSpeed);
        }
    }
}