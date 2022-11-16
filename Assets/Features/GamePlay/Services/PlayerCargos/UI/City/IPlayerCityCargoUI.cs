#region

using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.City
{
    public interface IPlayerCityCargoUI
    {
        bool IsActive { get; }

        void Open(IItem[] items);
        void Close();
    }
}