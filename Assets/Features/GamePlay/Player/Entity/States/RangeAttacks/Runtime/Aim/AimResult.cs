namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Aim
{
    public class AimResult
    {
        public AimResult(AimResultType type)
        {
            Type = type;
        } 
        
        public AimResult(float angle, float spread, AimResultType type)
        {
            Angle = angle;
            Spread = spread;
            Type = type;
        }
        
        public readonly float Angle;
        public readonly float Spread;
        public readonly AimResultType Type;
    }
}