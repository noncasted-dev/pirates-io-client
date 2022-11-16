#region

using Common.ObjectsPools.Runtime.Abstract;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Network.Remote.Bootstrap;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Services.PlayerSpawn.RemoteBuilders.Runtime
{
    public class RemoteViewFactory : IObjectFactory<PlayerRemoteView>
    {
        public RemoteViewFactory(
            AssetReference reference,
            Transform parent,
            IAssetInstantiatorFactory instantiatorFactory)
        {
            _reference = reference;
            _parent = parent;
            _instantiatorFactory = instantiatorFactory;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly Transform _parent;

        private readonly AssetReference _reference;

        private IAssetInstantiator<PlayerRemoteView> _instantiator;

        public async UniTask PreloadAsync()
        {
            _instantiator = _instantiatorFactory.Create<PlayerRemoteView>(_reference);
            await _instantiator.PreloadAsync();
        }

        public void Unload()
        {
            _instantiator.Release();
        }

        public PlayerRemoteView Create(Vector2 position, float angle = 0)
        {
            var view = _instantiator.Instantiate(position, angle, _parent);

            return view;
        }
    }
}