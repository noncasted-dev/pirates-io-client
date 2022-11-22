using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public interface IPriceProvider
    {
        void Freeze(ItemType type);
        void Unfreeze(ItemType type);
        void UnfreezeAll();

        int GetPrice(ItemType type);

        SellPrice GetPlayerSellPrice(ItemType type, int count);
        SellPrice GetStockSellPrice(ItemType type, int count);
    }
}