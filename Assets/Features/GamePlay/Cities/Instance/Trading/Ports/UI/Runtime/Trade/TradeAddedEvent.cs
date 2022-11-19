using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeAddedEvent
    {
        public TradeAddedEvent(ItemType type, ItemOrigin origin, int value)
        {
            Type = type;
            Origin = origin;
            Value = value;
        }

        public readonly ItemType Type;
        public readonly ItemOrigin Origin;
        public readonly int Value;
    }
}