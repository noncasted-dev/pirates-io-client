namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public readonly struct AimStartedEvent
    {
        public AimStartedEvent(IAimHandle handle)
        {
            Handle = handle;
        }
        
        public readonly IAimHandle Handle;
    }
}