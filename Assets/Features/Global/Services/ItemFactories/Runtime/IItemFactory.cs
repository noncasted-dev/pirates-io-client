#region

using GamePlay.Items.Abstract;

#endregion

namespace Global.Services.ItemFactories.Runtime
{
    public interface IItemFactory
    {
        IItem Create(ItemType type, int count);
    }
}