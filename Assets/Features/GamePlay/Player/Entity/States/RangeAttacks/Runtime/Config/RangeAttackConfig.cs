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
        
        public AimParams CreateAimParams()
        {
            return new AimParams(
                _asset.StartAimAngle,
                _asset.EndAimAngle,
                _asset.AimTime,
                _asset.AimOverTime,
                _asset.AimAdditionalSpread);
        }

        public ImpactParams CreateImpactParams()
        {
            return new ImpactParams(_asset.ImpactDistance, _asset.ImpactTime);
        }
    }
}