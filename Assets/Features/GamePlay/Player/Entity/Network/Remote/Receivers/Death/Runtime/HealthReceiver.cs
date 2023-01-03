using Common.ObjectsPools.Runtime.Abstract;
using Common.VFX;
using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using Ragon.Client;
using UnityEngine;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Death.Runtime
{
    public class HealthReceiver
    {
        public HealthReceiver(
            FireController controller,
            IPlayerEventListener listener,
            IObjectProvider<DeadShipVfx> deadShip,
            Transform transform)
        {
            _controller = controller;
            _deadShip = deadShip;
            _transform = transform;

            listener.AddListener<HealthChangeNetworkEvent>(OnHealthReceived);
        }
        
        private readonly FireController _controller;
        private readonly IObjectProvider<DeadShipVfx> _deadShip;
        private readonly Transform _transform;

        private void OnHealthReceived(RagonPlayer player, HealthChangeNetworkEvent data)
        {
            var delta = data.Current / (float)data.Max;
            Debug.Log($"<color=red>Health received: {data.Current} / {data.Max}</color>");
            _controller.SetFireForce(delta);

            if (data.Current <= 0)
                _deadShip.Get(_transform.position);
        }
    }
}