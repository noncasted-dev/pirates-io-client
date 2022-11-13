namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    public struct AimParams
    {
        public AimParams(
            float startAngle,
            float endAngle,
            float time,
            float overTime,
            float additionalSpread)
        {
            StartAngle = startAngle;
            EndAngle = endAngle;
            Time = time;
            OverTime = overTime;
            AdditionalSpread = additionalSpread;
        }
        
        public readonly float StartAngle;
        public readonly float EndAngle;
        public readonly float Time;     
        public readonly float OverTime;
        public readonly float AdditionalSpread;
    }
}