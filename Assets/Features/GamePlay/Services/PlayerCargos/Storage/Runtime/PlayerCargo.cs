using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
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
            IDroppedObjectsPresenter droppedObjectsPresenter)
        {
            _droppedObjectsPresenter = droppedObjectsPresenter;
        }

        private IDroppedObjectsPresenter _droppedObjectsPresenter;

        private PlayerCargoStorage _storage;

        private void Awake()
        {
            _storage = GetComponent<PlayerCargoStorage>();
        }

        public void OnDropped(IItem item, int count)
        {
            _droppedObjectsPresenter.DropFromPlayer(item.BaseData.Type, count);

            var type = item.BaseData.Type;

            _storage.Reduce(type, count);
        }
    }
}