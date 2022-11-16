using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public interface IPlayerTravelCargoUI
    {
        bool IsActive { get; }

        event Action<IItem, int, Action<IItem[]>> Dropped;

        Action<IItem[]> Open(IItem[] items);
        void Close();
    }
}