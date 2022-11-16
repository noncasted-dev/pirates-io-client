#region

using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Player.Entity.Setup.Flow.Callbacks;
using GamePlay.Player.Entity.Setup.Root;
using GamePlay.Player.Entity.Views.WeaponsRoots.Runtime;
using GamePlay.Player.Entity.Weapons.Cannon.Root;
using GamePlay.Player.Entity.Weapons.Common.Bootstrap.Runtime;
using GamePlay.Player.Entity.Weapons.Handler.Logs;
using Global.Services.AssetsFlow.Runtime.Abstract;
using UnityEngine.AddressableAssets;

#endregion

namespace GamePlay.Player.Entity.Weapons.Handler.Runtime
{
    public class WeaponsFactory : IWeaponsFactory, IDestroyCallback
    {
        public WeaponsFactory(
            PlayerScope scope,
            IAssetInstantiatorFactory instantiatorFactory,
            IWeaponsRoot weaponsRoot,
            WeaponsHandlerLogger logger)
        {
            _scope = scope;
            _instantiatorFactory = instantiatorFactory;
            _weaponsRoot = weaponsRoot;
            _logger = logger;
        }

        private readonly IAssetInstantiatorFactory _instantiatorFactory;
        private readonly WeaponsHandlerLogger _logger;

        private readonly PlayerScope _scope;

        private readonly List<IAssetInstantiator<IWeaponBootstrapper>> _weapons = new();
        private readonly IWeaponsRoot _weaponsRoot;

        public void OnDestroyed()
        {
            foreach (var weapon in _weapons)
                weapon.Release();

            _weapons.Clear();
        }

        public async UniTask<ICanon> CreateBow(AssetReference reference)
        {
            var instantiator = _instantiatorFactory.Create<IWeaponBootstrapper>(reference);
            _weapons.Add(instantiator);

            var weapon = await instantiator.InstantiateAsync(_weaponsRoot.Position);
            weapon.ToChild(_weaponsRoot.Transform);
            var result = await weapon.Build(_scope);

            _logger.OnInstantiated(result.Name);

            return (ICanon)result;
        }
    }
}