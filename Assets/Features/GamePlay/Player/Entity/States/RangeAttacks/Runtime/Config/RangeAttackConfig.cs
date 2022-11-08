using GamePlay.Player.Entity.States.RangeAttacks.Runtime.Dash;

namespace GamePlay.Player.Entity.States.RangeAttacks.Runtime.Config
{
    public class RangeAttackConfig : IRangeAttackConfig
    {
        public RangeAttackConfig(RangeAttackConfigAsset asset)
        {
            _asset = asset;
        }

        private readonly RangeAttackConfigAsset _asset;

        public float Delay => _asset.Delay;

        public DashParams CreateDashParams()
        {
            return new DashParams(_asset.DashDistance, _asset.DashTime, _asset.DashCurve);
        }
    }
}