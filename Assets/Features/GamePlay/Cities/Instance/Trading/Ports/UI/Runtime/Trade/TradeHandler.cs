using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeHandler : MonoBehaviour
    {
        [SerializeField] private Slider _progress;
        [SerializeField] private TMP_Text _sliderValue;
        
        private readonly Dictionary<ItemType, int> _player = new();
        private readonly Dictionary<ItemType, int> _stock = new();

        private IDisposable _playerListener;
        private IDisposable _stockListener;
        
        private void OnEnable()
        {
            _playerListener = MessageBroker.Default.Receive<TradeAddedEvent>().Subscribe(OnTradeAdd);
            _stockListener = MessageBroker.Default.Receive<TradeRemovedEvent>().Subscribe(OnTradeRemoved);
        }
        
        private void OnDisable()
        {
            _player.Clear();
            _stock.Clear();
            
            _playerListener?.Dispose();
            _stockListener?.Dispose();
        }

        private void OnTradeAdd(TradeAddedEvent data)
        {
            switch (data.Origin)
            {
                case ItemOrigin.Cargo:
                    _player[data.Type] = data.Value;
                    break;
                case ItemOrigin.Stock:
                    _stock[data.Type] = data.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var player = CalculateCost(_player);
            var stock = CalculateCost(_stock);

            _progress.minValue = -stock;
            _progress.maxValue = player;

            var value = player - stock;

            _progress.value = value;

            _sliderValue.text = $"{Mathf.Abs(value)}";
        }

        private void OnTradeRemoved(TradeRemovedEvent data)
        {
            switch (data.Origin)
            {
                case ItemOrigin.Cargo:
                    _player.Remove(data.Type);
                    break;
                case ItemOrigin.Stock:
                    _player.Remove(data.Type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var player = CalculateCost(_player);
            var stock = CalculateCost(_stock);

            _progress.minValue = player;
            _progress.maxValue = -stock;

            var value = player - stock;

            _progress.value = value;

            _sliderValue.text = $"{Mathf.Abs(value)}";
        }

        private int CalculateCost(IReadOnlyDictionary<ItemType, int> trades)
        {
            var trade = 0;

            foreach (var (_, value) in trades)
                trade += value;

            return trade;
        }
    }
}