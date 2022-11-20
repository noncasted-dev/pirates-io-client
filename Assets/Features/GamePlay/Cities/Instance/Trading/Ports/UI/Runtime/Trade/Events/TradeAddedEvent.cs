using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events
{
    public readonly struct TradeAddedEvent
    {
        public TradeAddedEvent(
            IItem item,
            ItemOrigin origin,
            int totalPrice,
            int count)
        {
            Item = item;
            Type = item.BaseData.Type;
            Origin = origin;
            TotalPrice = totalPrice;
            Count = count;
        }

        public readonly IItem Item;
        public readonly ItemType Type;
        public readonly ItemOrigin Origin;
        public readonly int TotalPrice;
        public readonly int Count;
    }
}