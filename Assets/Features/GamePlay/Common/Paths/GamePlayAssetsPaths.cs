namespace GamePlay.Common.Paths
{
    public class GamePlayAssetsPaths
    {
        public const string Root = "GamePlay/";
        public const string Config = Root + "Config/";

        public const string LogsPrefix = "LogSettings_";
        public const string ServicePrefix = "LocalService_";
        public const string NetworkPrefix = "SessionNetworkService_";
        public const string ConfigPrefix = "LocalConfig_";
        public const string CityPrefix = "City_";
        public const string ItemPrefix = "Item_";

        private const string _services = Root + "Services/";
        private const string _network = _services + "Network/";
        private const string _cities =  Root + "Cities/";
        private const string _factions =  Root + "Factions/";

        public const string LevelCamera = _services + "LevelCamera/";
        public const string LevelLoop = _services + "LevelLoop/";
        public const string PlayerFactory = _services + "PlayerFactory/";
        public const string Projectiles = _services + "Projectiles/";
        public const string TransitionScreen = _services + "TransitionScreen/";
        public const string Environment = _services + "Environment/";
        public const string NetworkBootstrapper = _network + "Bootstrapper/";
        public const string RemotePlayerBuilder = _network + "RemotePlayerBuilder/";
        public const string ProjectilesReplicator = _network + "ProjectilesReplicator/";
        public const string ChunkConfig = Config + "ChunkConfig";
        public const string VFX = _services + "VFX/";
        public const string PlayerPositionProvider = _services + "PlayerPositionProvider/";

        public const string CityDefinition = _cities + "Definition";
        public const string FactionSelection = _factions + "Selection";
        public const string Items = Root + "Items/";
    }
}