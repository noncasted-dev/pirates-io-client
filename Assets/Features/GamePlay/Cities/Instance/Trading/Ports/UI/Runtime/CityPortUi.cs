using System;
using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using Global.Services.Profiles.Storage;
using Global.Services.UiStateMachines.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
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
            UiConstraints constraints)
        {
            _profileStorageProvider = profileStorageProvider;
            _constraints = constraints;
            _stateMachine = stateMachine;
        }
        
        [SerializeField] private GameObject _body;

        [SerializeField] private TMP_Text _nickName;
        
        [SerializeField] private AvailableItemsList _cargoView;
        [SerializeField] private AvailableItemsList _stockView;
        
        [SerializeField] private TradeHandler _tradeHandler;
        [SerializeField] private GameObject _tradeBody;
        
        private IDisposable _enterListener;
        private IDisposable _exitListener;
        private IDisposable _requestListener;
        
        private UiConstraints _constraints;

        private IUiStateMachine _stateMachine;
        private IProfileStorageProvider _profileStorageProvider;
        private IPlayerCargoStorage _playerCargoStorage;

        public UiConstraints Constraints => _constraints;
        public string Name => "Port";

        private void Awake()
        {
            _body.SetActive(false);
            
            _tradeBody.SetActive(false);
        }

        private void OnEnable()
        {
            _enterListener = MessageBroker.Default.Receive<PortEnteredEvent>().Subscribe(OnEntered);
            _exitListener = MessageBroker.Default.Receive<PortExitedEvent>().Subscribe(OnExited);
            _requestListener = MessageBroker.Default.Receive<TradeRequestedEvent>().Subscribe(OnTradeRequested);
        }

        private void OnDisable()
        {
            _enterListener?.Dispose();
            _exitListener?.Dispose();
            _requestListener?.Dispose();
        }
        
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
            _nickName.text = _profileStorageProvider.UserName;
            
            _body.SetActive(true);
            _stateMachine.EnterAsSingle(this);
            
            _cargoView.Fill(data.Cargo, data.PriceProvider);
            _stockView.Fill(data.Stock, data.PriceProvider);
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
    }
}