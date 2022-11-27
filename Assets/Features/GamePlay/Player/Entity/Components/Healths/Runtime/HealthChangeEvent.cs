namespace GamePlay.Player.Entity.Components.Healths.Runtime
{
    public readonly struct HealthChangeEvent
    {
        public HealthChangeEvent(int current, int max)
        {
            Current = current;
            Max = max;
        }

        public readonly int Current;
        public readonly int Max;
    }
}