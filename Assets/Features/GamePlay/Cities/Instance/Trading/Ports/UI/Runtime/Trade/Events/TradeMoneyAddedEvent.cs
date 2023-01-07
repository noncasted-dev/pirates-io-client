namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events
{
    public readonly struct TradeMoneyAddedEvent
    {
        public TradeMoneyAddedEvent(ItemOrigin origin, int amount)
        {
            Origin = origin;
            Amount = amount;
        }

        public readonly ItemOrigin Origin;
        public readonly int Amount;
    }
}