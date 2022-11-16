using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    public interface IDroppedObjectsPresenter
    {
        void DropFromPlayer(ItemType type, int count);
        void Drop(ItemType type, int count, Vector2 position);
    }
}