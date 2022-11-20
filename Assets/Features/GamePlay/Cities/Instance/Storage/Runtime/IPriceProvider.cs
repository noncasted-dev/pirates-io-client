using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Storage.Runtime
{
    public interface IPriceProvider
    {
        void Freeze(ItemType type);
        void Unfreeze(ItemType type);
        
        int GetPrice(ItemType type);
        
        int GetPlayerSellPrice(ItemType type, int count);
        int GetStockSellPrice(ItemType type, int count);
    }
}