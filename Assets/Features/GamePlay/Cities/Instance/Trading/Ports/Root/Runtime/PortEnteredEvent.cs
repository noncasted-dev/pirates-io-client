using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public readonly struct PortEnteredEvent
    {
        public PortEnteredEvent(
            IReadOnlyList<IItem> cargo,
            IReadOnlyList<IItem> stock,
            IReadOnlyList<IItem> ships,
            IPriceProvider priceProvider,
            IShipResources shipResources,
            ICityStorage cityStorage)
        {
            PriceProvider = priceProvider;
            Cargo = cargo;
            Ships = ships;
            Stock = stock;
            ShipResources = shipResources;
            CityStorage = cityStorage;
        }

        public readonly IReadOnlyList<IItem> Cargo;
        public readonly IReadOnlyList<IItem> Stock;
        public readonly IReadOnlyList<IItem> Ships;
        public readonly IPriceProvider PriceProvider;
        public readonly IShipResources ShipResources;
        public readonly ICityStorage CityStorage;
    }
}