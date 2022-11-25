using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public interface IPlayerCargo
    {
        void OnDropped(IItem item, int count);
    }
}