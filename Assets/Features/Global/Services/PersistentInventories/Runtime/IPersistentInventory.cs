using GamePlay.Cities.Instance.Root.Runtime;
using GamePlay.Items.Abstract;

namespace Global.Services.PersistentInventories.Runtime
{
    public interface IPersistentInventory
    {
        void StoreItem(CityType city, StoredItem item);
        StoredItemsDictionary GetItems(CityType city);
    }
}