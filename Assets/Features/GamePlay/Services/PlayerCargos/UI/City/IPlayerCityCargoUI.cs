using System;
using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.UI.City
{
    public interface IPlayerCityCargoUI
    {
        bool IsActive { get; }

        Action<IItem[]> Open(IItem[] items);
        void Close();
    }
}