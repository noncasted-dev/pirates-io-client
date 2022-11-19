using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public interface IPriceProvider
    {
        void Freeze(ItemType type);
        void Unfreeze(ItemType type);
        int GetPrice(ItemType type);
        int GetSellPrice(ItemType type, int count);
    }
}