namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class PortEnteredEvent
    {
        public PortEnteredEvent(
            TradableItem[] cargo,
            TradableItem[] stock)
        {
            Cargo = cargo;
            Stock = stock;
        }
        
        public readonly TradableItem[] Cargo;
        public readonly TradableItem[] Stock;
    }
}