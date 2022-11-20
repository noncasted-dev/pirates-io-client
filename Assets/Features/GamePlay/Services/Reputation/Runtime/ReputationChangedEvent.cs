namespace GamePlay.Services.Reputation.Runtime
{
    public readonly struct ReputationChangedEvent
    {
        public ReputationChangedEvent(int reputation)
        {
            Reputation = reputation;
        }
        
        public readonly int Reputation;
    }
}