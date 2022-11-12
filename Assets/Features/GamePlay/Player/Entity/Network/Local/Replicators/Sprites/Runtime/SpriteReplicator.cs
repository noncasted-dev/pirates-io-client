using GamePlay.Player.Entity.Network.Root.Runtime;

namespace GamePlay.Player.Entity.Network.Local.Replicators.Sprites.Runtime
{
    public class SpriteReplicator : ISpriteReplicator
    {
        public SpriteReplicator(INetworkEventSender sender)
        {
            _sender = sender;
        }
        
        private readonly INetworkEventSender _sender;
        
        public void OnFlip(bool isFlipped)
        {
                
        }
    }
}