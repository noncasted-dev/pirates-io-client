using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Stock.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class CityPort : MonoBehaviour
    {
        [Inject]
        private void Construct(IPlayerCargoStorage playerCargoStorage)
        {
            _playerCargoStorage = playerCargoStorage;
        }

        [SerializeField] private CityStock _stock;
        
        private IPlayerCargoStorage _playerCargoStorage;

        public void Enter()
        {
            var stock = ToTradables(_stock.GetItems());
            var cargoRaw = _playerCargoStorage.ToArray();
            var cargo = ToTradables(_playerCargoStorage.Items);

            var data = new PortEnteredEvent(cargo, stock);

            MessageBroker.Default.Publish(data);
        }

        public void Exit()
        {
            var data = new PortExitedEvent();

            MessageBroker.Default.Publish(data);
        }

        private TradableItem[] ToTradables(IReadOnlyDictionary<ItemType, IItem> items)
        {
            var tradables = new TradableItem[items.Count];
            var counter = 0;

            foreach (var item in items)
            {
                tradables[counter] = new TradableItem(item.Value, 1);
                counter++;
            }

            return tradables;
        }
    }
}