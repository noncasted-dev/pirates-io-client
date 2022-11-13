using Global.Services.Network.Instantiators.Runtime;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Views.Transforms.Runtime
{
    [DisallowMultipleComponent]
    public class NetworkTransform : RagonBehaviour, INetworkTransform
    {
        private Vector2 _lastReplicated;

        private Vector2 _localPosition;
        private readonly RagonVector3 _position = new(Vector3.zero, RagonAxis.XY);

        public void SetPosition(Vector2 position)
        {
            _localPosition = position;
            transform.position = position;
        }

        public override void OnCreatedEntity()
        {
            var payload = Entity.GetSpawnPayload<NetworkPayload>();
            _position.Value = payload.Position;
        }

        public override void OnEntityTick()
        {
            if (_localPosition == _lastReplicated)
                return;

            _lastReplicated = _localPosition;
            _position.Value = _localPosition;
        }

        public override void OnProxyTick()
        {
            transform.position = _position.Value;
        }
    }
}