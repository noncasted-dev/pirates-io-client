#region

using GamePlay.Items.Abstract;
using UnityEngine;

#endregion

namespace GamePlay.Services.ObjectDroppers.Presenter.Runtime
{
    public interface IObjectDropper
    {
        void DropFromPlayer(IItem item);
        void Drop(IItem item, Vector2 position);
    }
}