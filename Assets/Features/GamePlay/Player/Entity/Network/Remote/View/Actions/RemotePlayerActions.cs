using Features.GamePlay.Player.Entity.Network.Common.Events;
using Ragon.Client;

namespace Features.GamePlay.Player.Entity.Network.Remote.View.Actions
{
    public class RemotePlayerActions : RagonBehaviour, IRemoteActions
    {
        public void StartBoarding()
        {
            var data = new StartBoardingNetworkEvent();
            ReplicateEvent(data);
        }

        public void StopBoarding()
        {
            var data = new StopBoardingNetworkEvent();
            ReplicateEvent(data);
        }
    }
}