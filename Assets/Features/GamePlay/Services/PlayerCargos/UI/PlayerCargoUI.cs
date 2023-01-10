using System;
using Common.Local.Services.Abstract.Callbacks;
using Cysharp.Threading.Tasks;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Views;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Events;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerCargos.UI.Events;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.InputViews.Runtime;
using Global.Services.MessageBrokers.Runtime;
using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerCargos.UI
{
    public class PlayerCargoUI : MonoBehaviour, ILocalSwitchListener, IUiState
    {
        [Inject]
        private void Construct(
            IInputView inputView,
            IPlayerCargoStorage storage,
            IPlayerEntityProvider entityProvider,
            IPlayerCargo cargo,
            IReputation reputation,
            IUiStateMachine uiStateMachine,
            UiConstraints constraints)
        {
            _constraints = constraints;
            _uiStateMachine = uiStateMachine;
            _cargo = cargo;
            _reputation = reputation;
            _entityProvider = entityProvider;
            _storage = storage;
            _inputView = inputView;
        }

        [SerializeField] private GameObject _body;
        [SerializeField] private CargoItemsListView _grid;
        [SerializeField] private DropConfirmation _drop;
        [SerializeField] private CargoShipView _shipView;
        [SerializeField] private MoneyView _money;

        private IPlayerCargo _cargo;

        private IDisposable _cargoChangedListener;

        private UiConstraints _constraints;
        private IDisposable _dropListener;
        private IPlayerEntityProvider _entityProvider;

        private IInputView _inputView;
        private IReputation _reputation;
        private IPlayerCargoStorage _storage;
        private IUiStateMachine _uiStateMachine;
        
        public MoneyView MoneyView => _money;

        private void Start()
        {
            _body.SetActive(false);
        }

        public void OnEnabled()
        {
            _inputView.InventoryPerformed += OnInventoryOpenPerformed;

            _cargoChangedListener = Msg.Listen<CargoChangedEvent>(OnCargoChanged);
            _dropListener = Msg.Listen<ItemDropRequestedEvent>(OnDropRequested);
        }

        public void OnDisabled()
        {
            _inputView.InventoryPerformed -= OnInventoryOpenPerformed;

            _cargoChangedListener.Dispose();
            _dropListener.Dispose();
        }

        public UiConstraints Constraints => _constraints;
        public string Name => "Cargo";

        public void Recover()
        {
            _shipView.Setup(_entityProvider.Resources, _reputation);
            _body.SetActive(true);
            _grid.Fill(_storage.ToArray());
        }

        public void Exit()
        {
            _body.SetActive(false);
        }

        private void OnCargoChanged(CargoChangedEvent data)
        {
            if (_body.activeSelf == false)
                return;

            Redraw(data.ToArray());
        }

        private void OnInventoryOpenPerformed()
        {
            if (_body.activeSelf == true)
                Close();
            else
                Open();
        }

        public void Switch()
        {
            if (_body.activeSelf == false)
                Open();
            else
                Close();
        }

        public void Open()
        {
            _uiStateMachine.EnterAsStack(this);

            _shipView.Setup(_entityProvider.Resources, _reputation);
            _body.SetActive(true);
            _grid.Fill(_storage.ToArray());
        }

        public void Close()
        {
            _uiStateMachine.Exit(this);

            _body.SetActive(false);
        }

        private void Redraw(IItem[] items)
        {
            _shipView.Setup(_entityProvider.Resources, _reputation);
            _grid.Fill(items);
            _drop.Cancel();
        }

        private void OnDropRequested(ItemDropRequestedEvent dropRequest)
        {
            ProcessDrop(dropRequest).Forget();
        }

        private async UniTaskVoid ProcessDrop(ItemDropRequestedEvent dropRequest)
        {
            var result = await _drop.Confirm(dropRequest.Item);

            switch (result.Type)
            {
                case DropConfirmationResultType.Applied:
                    _cargo.OnDropped(dropRequest.Item, result.Count);
                    break;
                case DropConfirmationResultType.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}