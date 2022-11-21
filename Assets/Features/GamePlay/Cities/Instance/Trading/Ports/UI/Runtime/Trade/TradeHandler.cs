using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.Reputation.Runtime;
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
            IWalletPresenter walletPresenter,
            IWallet wallet,
            IReputationPresenter reputationPresenter)
        {
            _reputationPresenter = reputationPresenter;
            _wallet = wallet;
            _walletPresenter = walletPresenter;
            _cargo = cargo;
        }

        [SerializeField] private Slider _progress;
        [SerializeField] private TMP_Text _playerTotal;
        [SerializeField] private TMP_Text _cityTotal;
        [SerializeField] private TMP_Text _delta;

        [SerializeField] private TradeApplyButton _applyApplyButton;

        [SerializeField] private TradeMoney _playerMoneyView;
        [SerializeField] private TradeMoney _cityMoneyView;

        private readonly Dictionary<ItemType, TradeLot> _player = new();
        private readonly Dictionary<ItemType, TradeLot> _stock = new();

        private int _cityMoney;
        private int _playerMoney;
        private int _playerTotalMoney;

        private IDisposable _playerListener;
        private IDisposable _stockListener;

        private IPlayerCargoStorage _cargo;
        private IWalletPresenter _walletPresenter;
        private IWallet _wallet;
        private ICityStorage _cityStorage;
        private IReputationPresenter _reputationPresenter;

        public bool IsActive => gameObject.activeSelf;

        public event Action Completed;

        public event Action<int> WeightUpdated;
        public event Action<int> CannonsUpdated;
        public event Action<int> TeamUpdated;
        public event Action<int> ReputationUpdated;

        private void OnEnable()
        {
            _playerListener = MessageBroker.Default.Receive<TradeAddedEvent>().Subscribe(OnTradeAdd);
            _stockListener = MessageBroker.Default.Receive<TradeRemovedEvent>().Subscribe(OnTradeRemoved);

            _delta.text = "0";

            _applyApplyButton.Clicked += OnApplyClicked;

            _playerMoney = 0;
            _cityMoney = 0;

            _playerMoneyView.SetAmount(0);
            _cityMoneyView.SetAmount(0);
        }

        private void OnDisable()
        {
            _player.Clear();
            _stock.Clear();

            _playerListener?.Dispose();
            _stockListener?.Dispose();

            _applyApplyButton.Clicked -= OnApplyClicked;
        }

        public void Setup(ICityStorage cityStorage)
        {
            _cityStorage = cityStorage;
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

        private void ApplyChanges()
        {
            var stock = CalculateCost(_stock);
            var player = CalculateCost(_player);

            var delta = player - stock;

            _playerMoney = 0;
            _cityMoney = 0;

            if (delta < 0)
            {
                if (_wallet.Money >= Mathf.Abs(delta))
                {
                    _playerMoney = Mathf.Abs(delta);
                    _cityMoney = 0;
                }
                else
                {
                    _playerMoney = _wallet.Money;
                    _cityMoney = 0;
                }
            }
            else
            {
                _cityMoney = delta;
            }

            _playerMoneyView.SetAmount(_playerMoney);
            _cityMoneyView.SetAmount(_cityMoney);

            var playerTotal = player + _playerMoney;
            var cityTotal = stock + _cityMoney;

            _playerTotal.text = $"{playerTotal}";
            _cityTotal.text = $"{cityTotal}";

            _playerTotalMoney = playerTotal;

            var value = playerTotal - cityTotal;

            if (value == 0)
            {
                _progress.minValue = 0;
                _progress.maxValue = 1;
                _progress.value = 0.5f;
            }
            else if (value > 0)
            {
                _progress.minValue = 0;
                _progress.maxValue = 1;
                _progress.value = 0.5f;
            }
            else
            {
                _progress.minValue = -stock;
                _progress.maxValue = playerTotal;
                _progress.value = value;
            }

            _delta.text = $"{value}";

            if (value >= 0)
                _applyApplyButton.OnAvailable();
            else
                _applyApplyButton.OnUnavailable();

            var weight = 0;
            var team = 0;
            var cannons = 0;

            foreach (var (_, lot) in _stock)
            {
                weight += lot.Count;

                switch (lot.Type)
                {
                    case ItemType.Cannon:
                        cannons += lot.Count;
                        break;
                    case ItemType.Team:
                        team += lot.Count;
                        break;
                }
            }

            foreach (var (_, lot) in _player)
            {
                weight += lot.Count;

                switch (lot.Type)
                {
                    case ItemType.Cannon:
                        cannons -= lot.Count;
                        break;
                    case ItemType.Team:
                        team -= lot.Count;
                        break;
                }
            }

            WeightUpdated?.Invoke(weight);
            TeamUpdated?.Invoke(team);
            CannonsUpdated?.Invoke(cannons);
            ReputationUpdated?.Invoke(_reputationPresenter.ConvertFromMoney(_playerTotalMoney));
        }

        private int CalculateCost(IReadOnlyDictionary<ItemType, TradeLot> trades)
        {
            var trade = 0;

            foreach (var (_, lot) in trades)
                trade += lot.TotalPrice;

            return trade;
        }

        private void OnApplyClicked()
        {
            _walletPresenter.Reduce(_playerMoney);
            _walletPresenter.Add(_cityMoney);

            var reputation = _reputationPresenter.ConvertFromMoney(_playerTotalMoney);
            _reputationPresenter.Add(reputation);

            foreach (var (_, lot) in _stock)
            {
                var copy = lot.Item.Copy();
                copy.SetCount(lot.Count);
                _cargo.Add(copy);

                lot.Item.Reduce(lot.Count);
            }

            foreach (var (_, lot) in _player)
            {
                var copy = lot.Item.Copy();
                copy.SetCount(lot.Count);

                _cityStorage.Add(copy);
                _cargo.Reduce(lot.Type, lot.Count);
            }

            _stock.Clear();
            _player.Clear();

            _playerMoney = 0;
            _cityMoney = 0;

            _playerMoneyView.SetAmount(0);
            _cityMoneyView.SetAmount(0);

            Completed?.Invoke();
        }
    }
}