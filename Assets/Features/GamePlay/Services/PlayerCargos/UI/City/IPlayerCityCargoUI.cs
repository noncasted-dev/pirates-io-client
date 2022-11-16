#region

using System;
using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.City
{
    public interface IPlayerCityCargoUI
    {
        bool IsActive { get; }

        Action<IItem[]> Open(IItem[] items);
        void Close();
    }
}