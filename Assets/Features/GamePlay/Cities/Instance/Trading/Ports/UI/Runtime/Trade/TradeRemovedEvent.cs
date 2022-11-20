using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeRemovedEvent
    {
        public TradeRemovedEvent(ItemType type, ItemOrigin origin)
        {
            Type = type;
            Origin = origin;
        }

        public readonly ItemType Type;
        public readonly ItemOrigin Origin;
    }
}