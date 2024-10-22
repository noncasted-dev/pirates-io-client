﻿using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Views;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.Profiles.Storage;
using Global.Services.Sounds.Runtime;
using Global.Services.UiStateMachines.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    [DisallowMultipleComponent]
    public class CityPortUi : MonoBehaviour, IUiState
    {
        [Inject]
        private void Construct(
            IUiStateMachine stateMachine,
            IProfileStorageProvider profileStorageProvider,
            IReputation reputation,
            UiConstraints constraints)
        {
            _reputation = reputation;
            _profileStorageProvider = profileStorageProvider;
            _constraints = constraints;
            _stateMachine = stateMachine;
        }

        [SerializeField] private GameObject _body;

        [SerializeField] private TMP_Text _nickName;

        [SerializeField] private StoredItemsList _storedView;
        [SerializeField] private StoredItemsList _stockView;
        [SerializeField] private StoredShipsList _stockShips;

        [SerializeField] private TradeItemsList _cargoTrade;
        [SerializeField] private TradeItemsList _stockTrade;

        [SerializeField] private TradeHandler _tradeHandler;
        [SerializeField] private GameObject _tradeBody;

        [SerializeField] private MoneyView _moneyView;
        [SerializeField] private PortShipView _shipView;
        [SerializeField] private TradeMoney _tradeMoney;

        [SerializeField] private Button _closeButton;

        private UiConstraints _constraints;

        private IDisposable _enterListener;
        private IDisposable _exitListener;
        private IPlayerCargoStorage _playerCargoStorage;
        private IProfileStorageProvider _profileStorageProvider;
        private IReputation _reputation;
        private IDisposable _requestListener;

        private IUiStateMachine _stateMachine;
        public MoneyView MoneyView => _moneyView;
        public TradeMoney TradeMoney => _tradeMoney;
        public TradeHandler TradeHandler => _tradeHandler;

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void OnEnable()
        {
            _enterListener = Msg.Listen<PortEnteredEvent>(OnEntered);
            _exitListener = Msg.Listen<PortExitedEvent>(OnExited);
            _requestListener = Msg.Listen<TradeRequestedEvent>(OnTradeRequested);

            _tradeBody.SetActive(false);

            _closeButton.onClick.AddListener(OnCloseClicked);

            _tradeHandler.Completed += OnTradeCompleted;
        }

        private void OnDisable()
        {
            _enterListener?.Dispose();
            _exitListener?.Dispose();
            _requestListener?.Dispose();

            _closeButton.onClick.RemoveListener(OnCloseClicked);

            _tradeHandler.Completed -= OnTradeCompleted;
        }

        public UiConstraints Constraints => _constraints;
        public string Name => "Port";

        public void Recover()
        {
            _body.SetActive(true);
        }

        public void Exit()
        {
            _body.SetActive(false);
        }

        private void OnEntered(PortEnteredEvent data)
        {
            _stateMachine.EnterAsSingle(this);

            _nickName.text = _profileStorageProvider.UserName;

            _body.SetActive(true);

            _storedView.gameObject.SetActive(false);
            _stockView.gameObject.SetActive(false);

            _storedView.gameObject.SetActive(true);
            _stockView.gameObject.SetActive(true);
            _tradeBody.SetActive(false);

            _storedView.Fill(data.Cargo, data.PriceProvider);

            _stockView.Fill(data.Stock, data.PriceProvider);
            _stockShips.Fill(data.Ships, _reputation);

            _cargoTrade.Setup(data.PriceProvider);
            _stockTrade.Setup(data.PriceProvider);

            _shipView.Setup(data.ShipResources, _reputation);
            _tradeHandler.Setup(data.CityStorage);

            MessageBrokerSoundExtensions.TriggerSound(SoundType.UiOpen);
        }

        private void OnExited(PortExitedEvent data)
        {
            _stateMachine.Exit(this);
        }

        private void OnTradeRequested(TradeRequestedEvent data)
        {
            if (_tradeHandler.IsActive == true)
                return;

            _tradeBody.SetActive(true);
        }

        private void OnTradeCompleted(TradeResult result)
        {
            _tradeBody.SetActive(false);

            var completed = new TradeCompletedEvent(Redraw, result);

            Msg.Publish(completed);
        }

        private void Redraw(
            IReadOnlyList<IItem> stock,
            IReadOnlyList<IItem> cargo,
            IReadOnlyList<IItem> ships,
            IPriceProvider priceProvider)
        {
            _shipView.ResetStats();

            _tradeBody.SetActive(false);

            _shipView.ResetStats();

            _storedView.Fill(cargo, priceProvider);
            _stockView.Fill(stock, priceProvider);
            _stockShips.Fill(ships, _reputation);
        }

        private void OnCloseClicked()
        {
            Msg.Publish(new PortClosedEvent());
        }
    }
}