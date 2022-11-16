using GamePlay.Services.DroppedObjects.Implementation.Items.Runtime;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Player.Entity.Views.ObjectsCollector.Runtime
{
    [DisallowMultipleComponent]
    public class PlayerObjectsCollector : MonoBehaviour
    {
        [Inject]
        private void Construct(
            IDroppedObjectsPresenter droppedObjectsPresenter,
            IPlayerCargoStorage cargo)
        {
            _cargo = cargo;
            _droppedObjectsPresenter = droppedObjectsPresenter;
        }

        private IPlayerCargoStorage _cargo;

        private IDroppedObjectsPresenter _droppedObjectsPresenter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDroppedItem droppedItem) == false)
            {
                Debug.LogError($"Wrong collectable trigger with: {other.name}");
                return;
            }

            _cargo.Add(droppedItem.Item);
            droppedItem.Collect();
        }
    }
}