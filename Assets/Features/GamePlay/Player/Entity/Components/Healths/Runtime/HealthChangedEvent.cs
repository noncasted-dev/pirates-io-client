namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public readonly struct HealthChangedEvent
    {
        public HealthChangedEvent(int current, int max)
        {
            Current = current;
            Max = max;
        }
        
        public readonly int Current;
        public readonly int Max;
    }
}