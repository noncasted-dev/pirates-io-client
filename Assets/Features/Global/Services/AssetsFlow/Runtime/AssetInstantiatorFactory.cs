using Global.Services.AssetsFlow.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Global.Services.AssetsFlow.Runtime
{
    public class AssetInstantiatorFactory : MonoBehaviour, IAssetInstantiatorFactory
    {
        [Inject]
        public void Construct(
            IAssetLoader loader,
            IAssetUnloader unloader,
            AssetsFlowLogger logger)
        {
            _loader = loader;
            _unloader = unloader;
            _logger = logger;
        }

        private IAssetLoader _loader;

        private AssetsFlowLogger _logger;
        private IAssetUnloader _unloader;

        public IAssetInstantiator<T> Create<T>(AssetReference reference) where T : class
        {
            var instantiator = new AssetInstantiator<T>(_loader, _unloader, _logger, reference);

            _logger.OnInstantiatorCreated(typeof(T).Name);

            return instantiator;
        }
    }
}