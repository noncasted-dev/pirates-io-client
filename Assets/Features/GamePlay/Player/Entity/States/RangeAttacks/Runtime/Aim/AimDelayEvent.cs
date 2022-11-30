namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public readonly struct AimDelayEvent
    {
        public AimDelayEvent(float delay)
        {
            Delay = delay;
        }
        
        public readonly float Delay;
    }
}