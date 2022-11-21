using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
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

        private bool _isActive;
        private IDisposable _completionListener;
        
        private IPlayerCargoStorage _playerCargoStorage;

        private void OnEnable()
        {
            _completionListener = MessageBroker.Default.Receive<TradeCompletedEvent>().Subscribe(OnTradeCompleted);
        }

        private void OnDisable()
        {
            _completionListener?.Dispose();
        }

        public void Enter(IShipResources shipResources)
        {
            _isActive = true;
            
            var stock = ToArray(_storage.Items);
            Debug.Log($"Cargo: {_playerCargoStorage.Items.Count}");
            var cargo = ToArray(_playerCargoStorage.Items);

            var data = new PortEnteredEvent(cargo, stock, _storage, shipResources, _storage);

            MessageBroker.Default.Publish(data);
        }

        public void Exit()
        {
            var data = new PortExitedEvent();

            MessageBroker.Default.Publish(data);

            _isActive = false;
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

        private void OnTradeCompleted(TradeCompletedEvent completed)
        {
            if (_isActive == false)
                return;
            
            _storage.UnfreezeAll();
            
            var stock = ToArray(_storage.Items);

            var cargo = ToArray(_playerCargoStorage.Items);

            completed.RedrawCallback?.Invoke(stock, cargo, _storage);
        }
    }
}