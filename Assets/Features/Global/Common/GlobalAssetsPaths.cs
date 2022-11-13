namespace Global.Common
{
    public static class GlobalAssetsPaths
    {
        private const string _root = "Global/";
        private const string _services = _root + "Services/";
        private const string _network = _services + "Network/";

        public const string Config = _root + "Config/";

        public const string ServicePrefix = "GlobalService_";
        public const string LogsPrefix = "LogSettings_";
        public const string ConfigPrefix = "GlobalConfig_";
        public const string BootstrapPrefix = "Bootstrap_";

        public const string BootstrapConfig = _root + "BootstrapConfig";

        public const string ApplicationProxy = _services + "ApplicationProxy/";
        public const string AssetsFlow = _services + "AssetsFlow/";
        public const string CameraUtils = _services + "CameraUtils/";
        public const string CurrentCamera = _services + "CurrentCamera/";
        public const string CurrentSceneHandler = _services + "CurrentSceneHandler/";
        public const string FilesFlow = _services + "FilesFlow/";
        public const string GlobalCamera = _services + "GlobalCamera/";
        public const string InputView = _services + "InputView/";
        public const string LoadingScreen = _services + "LoadingScreen/";
        public const string Logger = _services + "Logger/";
        public const string ResourceCleaner = _services + "ResourceCleaner/";
        public const string ScenesFlow = _services + "ScenesFlow/";
        public const string Updater = _services + "Updater/";
        public const string SceneObjects = _services + "SceneObjects/";
        public const string GameLoop = _services + "GameLoop/";
        public const string ObjectsPool = _services + "ObjectsPool/";
        public const string DebugConsole = _services + "DebugConsole/";

        public const string NetworkBootstrap = _network + "Bootstrap";
        public const string NetworkConnection = _network + "Connector/";
        public const string NetworkSession = _network + "Session/";
        public const string NetworkInstantiator = _network + "Instantiator/";
        public const string Profile = _services + "Profile/";
    }
}