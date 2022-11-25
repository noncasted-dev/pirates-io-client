using System;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerCargoStorage))]
    public class PlayerCargo : MonoBehaviour, IPlayerCargo
    {
        [Inject]
        private void Construct(
            IDroppedObjectsPresenter droppedObjectsPresenter,
            IPlayerEntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
            _dropper = droppedObjectsPresenter;
        }

        [SerializeField] private float _dropRange = 4f;

        private IDisposable _deathListener;
        
        private IDroppedObjectsPresenter _dropper;
        private IPlayerEntityProvider _entityProvider;

        private PlayerCargoStorage _storage;

        private void Awake()
        {
            _storage = GetComponent<PlayerCargoStorage>();
        }

        private void OnEnable()
        {
            _deathListener = MessageBroker.Default.Receive<PlayerDeathEvent>().Subscribe(OnDeath);
        }
        
        private void OnDisable()
        {
            _deathListener?.Dispose();
        }

        public void OnDropped(IItem item, int count)
        {
            _dropper.DropFromPlayer(item.BaseData.Type, count);

            var type = item.BaseData.Type;

            _storage.Reduce(type, count);
        }

        private void OnDeath(PlayerDeathEvent data)
        {
            var center = _entityProvider.Position;
            
            foreach (var (type, item) in _storage.Items)
            {
                var distance = Random.Range(0f, _dropRange);
                var direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                direction.Normalize();

                var position = center + direction * distance;
                _dropper.Drop(type, item.Count, position);
            }
            
            _storage.Clear();
        }
    }
}