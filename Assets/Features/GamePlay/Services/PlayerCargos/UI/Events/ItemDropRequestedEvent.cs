using GamePlay.Items.Abstract;

namespace GamePlay.Services.PlayerCargos.UI.Events
{
    public readonly struct ItemDropRequestedEvent
    {
        public ItemDropRequestedEvent(IItem item)
        {
            Item = item;
            Type = item.BaseData.Type;
        }
        
        public readonly IItem Item;
        public readonly ItemType Type;
    }
}