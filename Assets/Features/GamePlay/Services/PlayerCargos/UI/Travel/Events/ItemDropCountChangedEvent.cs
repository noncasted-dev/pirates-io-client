using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.UI.Travel.Events
{
    public class ItemDropCountChangedEvent
    {
        public ItemDropCountChangedEvent(IItem item, int count)
        {
            Count = count;
            Type = item.BaseData.Type;
        }
        
        public readonly ItemType Type;
        public readonly int Count;
    }
}