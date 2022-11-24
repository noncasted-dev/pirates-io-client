using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerCargoStorage))]
    public class PlayerCargo : MonoBehaviour
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

        private void OnDropped(IItem item, int count, Action<IItem[]> redrawCallback)
        {
            _droppedObjectsPresenter.DropFromPlayer(item.BaseData.Type, count);

            var type = item.BaseData.Type;

            _storage.Reduce(type, count);

            if (_storage.Items[type].Count == 0)
                _storage.Delete(type);

            redrawCallback?.Invoke(_storage.ToArray());
        }
    }
}