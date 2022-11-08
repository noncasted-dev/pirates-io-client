using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    public interface IRangeAttackConfig
    {
        float Delay { get; }
        DashParams CreateDashParams();
    }
}