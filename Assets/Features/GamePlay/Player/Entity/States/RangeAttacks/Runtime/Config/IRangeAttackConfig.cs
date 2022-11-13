namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    public interface IRangeAttackConfig
    {
        float Delay { get; }
        AimParams CreateAimParams();
        ImpactParams CreateImpactParams();
    }
}