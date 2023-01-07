using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.UI.Events
{
    public class ItemDropCountChangedEvent
    {
        public ItemDropCountChangedEvent(IItem item, int count)
        {
            Count = count;
            Type = item.BaseData.Type;
        }

        public readonly int Count;

        public readonly ItemType Type;
    }
}