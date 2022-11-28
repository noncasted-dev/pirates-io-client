using GamePlay.Factions.Common;

namespace GamePlay.Player.Entity.Network.Remote.Receivers.Damages.Runtime
{
    public readonly struct RemoteDamagedEvent
    {
        public RemoteDamagedEvent(FactionType faction)
        {
            Faction = faction;
        }
        
        public readonly FactionType Faction;
    }
}