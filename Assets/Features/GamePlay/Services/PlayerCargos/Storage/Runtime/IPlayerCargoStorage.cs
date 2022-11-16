#region

using GamePlay.Items.Abstract;

#endregion

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    public interface IPlayerCargoStorage
    {
        void Add(IItem item);
        void Reduce(ItemType type, int amount);
        void Delete(ItemType type);
        IItem[] ToArray();
    }
}