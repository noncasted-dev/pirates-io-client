using GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime;
using Ragon.Client;

namespace Features.GamePlay.Player.Entity.Network.Remote.BuildInvoker
{
    public class RemotePlayerBuildInvoker : RagonBehaviour
    {
        public override void OnCreatedEntity()
        {
            if (IsMine == true)
                return;
            
            RemotePlayerBuilder.Instance.Build(gameObject);
        }
    }
}