namespace GamePlay.Common.Paths
{
    public class GamePlayAssetsPaths
    {
        public const string Root = "GamePlay/";
        public const string Config = Root + "Config/";

        public const string LogsPrefix = "LogSettings_";
        public const string ServicePrefix = "LocalService_";
        public const string ConfigPrefix = "LocalConfig_";

        private const string _services = Root + "Services/";

        public const string LevelCamera = _services + "LevelCamera/";
        public const string LevelLoop = _services + "LevelLoop/";
        public const string PlayerFactory = _services + "PlayerFactory/";
        public const string Projectiles = _services + "Projectiles/";
        public const string TransitionScreen = _services + "TransitionScreen/";
        public const string Environment = _services + "Environment/";
    }
}