using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events
{
    public class TradeRemovedEvent
    {
        public TradeRemovedEvent(IItem item, ItemOrigin origin)
        {
            Item = item;
            Type = item.BaseData.Type;
            Origin = origin;
        }

        public readonly IItem Item;
        public readonly ItemType Type;
        public readonly ItemOrigin Origin;
    }
}