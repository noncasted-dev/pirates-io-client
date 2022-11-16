using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Global.Services.AssetsFlow.Tests
{
    public class AssetsFlowTestHandler : MonoBehaviour
    {
        public void Construct()
        {
            var flowLogger = new AssetsFlowLogger(new AssetsFlowTest.LoggerMock(), _logSettings);
            _loader.Construct(_storage, flowLogger);
            _unloader.Construct(_storage, flowLogger);
            _storage.Construct(flowLogger);
            _factory.Construct(_loader, _unloader, flowLogger);
        }

        [SerializeField] private AssetInstantiatorFactory _factory;
        [SerializeField] private AssetLoader _loader;

        [SerializeField] private AssetsFlowLogSettings _logSettings;
        [SerializeField] private AssetReference _reference;
        [SerializeField] private AssetsReferencesStorage _storage;
        [SerializeField] private AssetUnloader _unloader;

        public AssetsFlowLogSettings LogSettings => _logSettings;
        public AssetReference Reference => _reference;
        public AssetLoader Loader => _loader;
        public AssetUnloader Unloader => _unloader;
        public AssetsReferencesStorage Storage => _storage;
        public AssetInstantiatorFactory Factory => _factory;
    }
}