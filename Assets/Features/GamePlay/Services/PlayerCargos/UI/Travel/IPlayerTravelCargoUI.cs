#region

using System;
using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public interface IPlayerTravelCargoUI
    {
        bool IsActive { get; }

        event Action<Item, Action<Item[]>> Dropped;

        void Open(Item[] items);
        void Close();
    }
}