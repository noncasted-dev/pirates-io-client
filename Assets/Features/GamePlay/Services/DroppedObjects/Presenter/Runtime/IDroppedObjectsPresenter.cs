using GamePlay.Items.Abstract;
using UnityEngine;

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    public interface IDroppedObjectsPresenter
    {
        void DropFromPlayer(IItem item);
        void Drop(IItem item, Vector2 position);
    }
}