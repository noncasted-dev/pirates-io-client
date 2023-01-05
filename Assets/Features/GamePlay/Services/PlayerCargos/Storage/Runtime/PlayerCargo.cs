using System;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.States.Deaths.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using Global.Services.FilesFlow.Runtime.Abstract;
using Global.Services.MessageBrokers.Runtime;
using UniRx;
using UnityEngine;
using VContainer;

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
            _deathListener = Msg.Listen<PlayerDeathEvent>(OnDeath);
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
                _dropper.DropFromDeath(type, item.Count, center);
            
            _storage.Clear();
        }
    }
}