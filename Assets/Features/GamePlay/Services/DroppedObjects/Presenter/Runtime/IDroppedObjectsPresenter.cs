#region

using GamePlay.Items.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.DroppedObjects.Presenter.Runtime
{
    public interface IDroppedObjectsPresenter
    {
        void DropFromPlayer(IItem item);
        void Drop(IItem item, Vector2 position);
    }
}