namespace GamePlay.Player.Entity.States.Runs.Runtime
{
    public class RunConfig : IRunConfig
    {
        public RunConfig(RunConfigAsset asset)
        {
            _asset = asset;
        }

        private readonly RunConfigAsset _asset;

        public float Speed => _asset.Speed;
    }
}