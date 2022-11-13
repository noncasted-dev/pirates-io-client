using GamePlay.Player.Entity.Network.Root.Runtime;

namespace GamePlay.Player.Entity.Network.Local.Replicators.Sprites.Runtime
{
    public class SpriteReplicator : ISpriteReplicator
    {
        public SpriteReplicator(IPlayerEventSender sender)
        {
            _sender = sender;
        }

        private readonly IPlayerEventSender _sender;

        public void OnFlip(bool isFlipped)
        {
        }
    }
}