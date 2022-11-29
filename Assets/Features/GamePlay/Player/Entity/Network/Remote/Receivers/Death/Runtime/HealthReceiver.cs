using GamePlay.Player.Entity.Components.Healths.Runtime;
using GamePlay.Player.Entity.Network.Root.Runtime;
using Ragon.Client;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Death.Runtime
{
    public class HealthReceiver
    {
        public HealthReceiver(
            FireController controller,
            IPlayerEventListener listener)
        {
            _controller = controller;
            
            listener.AddListener<HealthChangeNetworkEvent>(OnHealthReceived);
        }
        
        private readonly FireController _controller;

        private void OnHealthReceived(RagonPlayer player, HealthChangeNetworkEvent data)
        {
            var delta = data.Current / data.Max;
            _controller.SetFireForce(delta);
        }
    }
}