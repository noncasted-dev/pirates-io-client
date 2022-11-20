using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct PortEnteredEvent
    {
        public PortEnteredEvent(
            IItem[] cargo,
            IItem[] stock,
            IPriceProvider priceProvider,
            IShipResources shipResources,
            ICityStorage cityStorage)
        {
            PriceProvider = priceProvider;
            Cargo = cargo;
            Stock = stock;
            ShipResources = shipResources;
            CityStorage = cityStorage;
        }

        public readonly IItem[] Cargo;
        public readonly IItem[] Stock;
        public readonly IPriceProvider PriceProvider;
        public readonly IShipResources ShipResources;
        public readonly ICityStorage CityStorage;
    }
}