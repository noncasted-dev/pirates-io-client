using Common.ObjectsPools.Runtime.Abstract;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.VFX.Pool.Implementation.Dead;
using GamePlay.Services.VFX.Pool.Provider;
using Global.Services.ItemFactories.Runtime;
using Global.Services.Updaters.Runtime.Abstract;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace GamePlay.Services.FishSpawn.Runtime
{
    public class FishSpawner : MonoBehaviour, IUpdatable, ILocalAwakeListener, ILocalSwitchListener
    {
        [Inject]
        private void Construct(
            IUpdater updater,
            IPlayerEntityProvider provider, 
            IVfxPoolProvider vfxPoolProvider,
            IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _vfxPoolProvider = vfxPoolProvider;
            _entity = provider;
            _updater = updater;
        }

        [SerializeField] private AssetReference _fish;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private int _dropCount = 10;
        
        private float _time;
        
        private IUpdater _updater;
        private IPlayerEntityProvider _entity;
        private IVfxPoolProvider _vfxPoolProvider;

        private IObjectProvider<FishVfx> _provider;
        private IItemFactory _itemFactory;

        public void OnAwake()
        {
            _provider = _vfxPoolProvider.GetPool<FishVfx>(_fish);
        }
        
        public void OnEnabled()
        {
            _updater.Add(this);
        }

        public void OnDisabled()
        {
            _updater.Remove(this);
        }
        
        public void OnUpdate(float delta)
        {
            _time += delta;
            
            if (_time < _spawnRate)
                return;

            _time = 0f;

            var direction = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
            var position = _entity.Position + direction * _spawnDistance;

            var fish = _provider.Get(position);
            var item = _itemFactory.Create(ItemType.Fish, _dropCount);

            fish.Drop(0, null, item, Vector2.zero, Vector2.zero);
        }
    }   
}