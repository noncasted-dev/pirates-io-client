#region

using System;
using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public interface IPlayerTravelCargoUI
    {
        bool IsActive { get; }

        event Action<IItem, Action<IItem[]>> Dropped;

        Action<IItem[]> Open(IItem[] items);
        void Close();
    }
}