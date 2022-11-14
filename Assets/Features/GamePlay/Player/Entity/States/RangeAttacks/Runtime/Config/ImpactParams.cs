namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    public struct ImpactParams
    {
        public ImpactParams(float distance, float time)
        {
            Distance = distance;
            Time = time;
        }

        public readonly float Distance;
        public readonly float Time;
    }
}