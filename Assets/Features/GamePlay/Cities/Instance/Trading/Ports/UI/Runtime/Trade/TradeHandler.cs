using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.Wallets.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeHandler : MonoBehaviour
    {
        [Inject]
        private void Construct(
            IPlayerCargoStorage cargo,
            IWalletPresenter wallet)
        {
            _wallet = wallet;
            _cargo = cargo;
        }
        
        [SerializeField] private Slider _progress;
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private TradeApplyButton _applyApplyButton;
        
        private readonly Dictionary<ItemType, TradeLot> _player = new();
        private readonly Dictionary<ItemType, TradeLot> _stock = new();

        private int _playerMoney;

        private IDisposable _playerListener;
        private IDisposable _stockListener;
        private IDisposable _moneyListener;
        
        private IPlayerCargoStorage _cargo;
        private IWalletPresenter _wallet;

        public bool IsActive => gameObject.activeSelf;

        public event Action Completed;

        private void OnEnable()
        {
            _playerListener = MessageBroker.Default.Receive<TradeAddedEvent>().Subscribe(OnTradeAdd);
            _stockListener = MessageBroker.Default.Receive<TradeRemovedEvent>().Subscribe(OnTradeRemoved);
            _moneyListener = MessageBroker.Default.Receive<TradeMoneyAddedEvent>().Subscribe(OnMoneyAdded);

            _sliderValue.text = "0";

            _applyApplyButton.Clicked += OnApplyClicked;
        }

        private void OnDisable()
        {
            _player.Clear();
            _stock.Clear();

            _playerListener?.Dispose();
            _stockListener?.Dispose();
            _moneyListener?.Dispose();
            
            _applyApplyButton.Clicked -= OnApplyClicked;
        }

        private void OnTradeAdd(TradeAddedEvent data)
        {
            switch (data.Origin)
            {
                case ItemOrigin.Cargo:
                    if (_player.ContainsKey(data.Type) == false)
                        _player[data.Type] = new TradeLot(data.Item);
                    
                    _player[data.Type].SetData(data.Count, data.TotalPrice);
                    break;
                case ItemOrigin.Stock:
                    if (_stock.ContainsKey(data.Type) == false)
                        _stock[data.Type] = new TradeLot(data.Item);
                    
                    _stock[data.Type].SetData(data.Count, data.TotalPrice);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ApplyChanges();
        }

        private void OnTradeRemoved(TradeRemovedEvent data)
        {
            switch (data.Origin)
            {
                case ItemOrigin.Cargo:
                    _player.Remove(data.Type);
                    break;
                case ItemOrigin.Stock:
                    _stock.Remove(data.Type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ApplyChanges();

            if (_player.Count == 0 && _stock.Count == 0)
                gameObject.SetActive(false);
        }
        
        private void OnMoneyAdded(TradeMoneyAddedEvent data)
        {
            _playerMoney = data.Origin switch
            {
                ItemOrigin.Cargo => data.Amount,
                ItemOrigin.Stock => data.Amount,
                _ => throw new ArgumentOutOfRangeException()
            };

            ApplyChanges();
        }

        private void ApplyChanges()
        {
            var player = CalculateCost(_player) + _playerMoney;
            var stock = CalculateCost(_stock);

            _progress.maxValue = player;
            _progress.minValue = -stock;

            var value = player - stock;

            _progress.value = value;

            _sliderValue.text = $"{value}";
            
            if (value > 0)
                _applyApplyButton.OnAvailable();
            else
                _applyApplyButton.OnUnavailable();
        }

        private int CalculateCost(IReadOnlyDictionary<ItemType, TradeLot> trades)
        {
            var trade = 0;

            foreach (var (_, lot) in trades)
            {
                Debug.Log($"Total price: {lot.TotalPrice}");
                trade += lot.TotalPrice;
            }

            return trade;
        }

        private void OnApplyClicked()
        {
            var player = CalculateCost(_player) + _playerMoney;
            var stock = CalculateCost(_stock);

            var value = player - stock;
            
            _wallet.Reduce(_playerMoney);
            _wallet.Add(value);
            
            foreach (var (_, lot) in _stock)
            {
                var copy = lot.Item.Copy();
                copy.SetCount(lot.Count);
                _cargo.Add(copy);
                
                lot.Item.Reduce(lot.Count);
            }

            foreach (var (_, lot) in _player)
                _cargo.Reduce(lot.Type, lot.Count);
            
            _stock.Clear();
            _player.Clear();

            _playerMoney = 0;
            
            Completed?.Invoke();
        }
    }
}