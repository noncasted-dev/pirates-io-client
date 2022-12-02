using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Items.Abstract;
using GamePlay.Player.Entity.Components.ShipResources.Runtime;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using Global.Services.Sounds.Runtime;
using UniRx;
using UnityEngine;

namespace GamePlay.Cities.Instance.Trading.Ports.Root.Runtime
{
    public class CityPort : MonoBehaviour
    {
        public void Construct(
            IPlayerCargoStorage playerCargoStorage)
        {
            _playerCargoStorage = playerCargoStorage;
        }

        [SerializeField] private CityStorage _storage;

        private bool _isActive;
        private IDisposable _completionListener;
        private IDisposable _closeListener;
        
        private IPlayerCargoStorage _playerCargoStorage;

        private void OnEnable()
        {
            _completionListener = MessageBroker.Default.Receive<TradeCompletedEvent>().Subscribe(OnTradeCompleted);
            _closeListener = MessageBroker.Default.Receive<PortClosedEvent>().Subscribe(OnClosed);
        }

        private void OnDisable()
        {
            _completionListener?.Dispose();
            _closeListener?.Dispose();
        }

        public void Enter(IShipResources shipResources)
        {
            if (_isActive == true) 
                return;
            
            Debug.Log($"Entered: {transform.parent.name}");
            
            _isActive = true;
            
            var stock = ToArray(_storage.Items);
            var cargo = ToArray(_playerCargoStorage.Items);
            var ships = ToArray(_storage.Ships);

            var data = new PortEnteredEvent(cargo, stock, ships, _storage, shipResources, _storage);

            MessageBroker.Default.Publish(data);
            
            MessageBroker.Default.TriggerSound(SoundType.PortEnter);
        }

        public void Exit()
        {
            if (_isActive == false)
                return;
            
            var data = new PortExitedEvent();
            MessageBroker.Default.Publish(data);
            
            MessageBroker.Default.TriggerSound(SoundType.PortExit);

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

            if (completed.Result.IsContainingShip == true)
            {
                var exited = new PortExitedEvent();
                var change = new ShipChangeEvent(completed.Result.Ship.Type);
                
                MessageBroker.Default.Publish(exited);
                MessageBroker.Default.Publish(change);
                return;
            }
            
            _storage.UnfreezeAll();
            
            var stock = ToArray(_storage.Items);
            var cargo = ToArray(_playerCargoStorage.Items);
            var ships = ToArray(_storage.Ships);
            
            completed.RedrawCallback?.Invoke(stock, cargo, ships, _storage);
        }

        private void OnClosed(PortClosedEvent data)
        {
            if (_isActive == false)
                return;
            
            Exit();
        }
    }
}