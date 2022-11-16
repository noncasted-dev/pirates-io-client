using GamePlay.Items.Abstract;

namespace Global.Services.ItemFactories.Runtime
{
    public interface IItemFactory
    {
        IItem Create(ItemType type, int count);
    }
}