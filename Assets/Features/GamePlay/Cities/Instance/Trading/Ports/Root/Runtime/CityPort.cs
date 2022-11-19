using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
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

        [SerializeField] private CityStorage _storage;
        
        private IPlayerCargoStorage _playerCargoStorage;

        public void Enter()
        {
            var stock = ToArray(_storage.Items);
            var cargo = ToArray(_playerCargoStorage.Items);

            var data = new PortEnteredEvent(cargo, stock, _storage);

            MessageBroker.Default.Publish(data);
        }

        public void Exit()
        {
            var data = new PortExitedEvent();

            MessageBroker.Default.Publish(data);
        }

        private IItem[] ToArray(IReadOnlyDictionary<ItemType, IItem> items)
        {
            var array = new IItem[items.Count];
            var counter = 0;

            foreach (var item in items)
            {
                array[counter] = item.Value;
                counter++;
            }

            return array;
        }
    }
}