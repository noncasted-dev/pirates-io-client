using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class PortEnteredEvent
    {
        public PortEnteredEvent(
            IItem[] cargo,
            IItem[] stock,
            IPriceProvider priceProvider)
        {
            PriceProvider = priceProvider;
            Cargo = cargo;
            Stock = stock;
        }
        
        public readonly IItem[] Cargo;
        public readonly IItem[] Stock;
        public IPriceProvider PriceProvider;
    }
}