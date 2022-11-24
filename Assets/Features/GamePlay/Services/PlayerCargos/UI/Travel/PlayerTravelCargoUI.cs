using System;
using Cysharp.Threading.Tasks;
using Features.GamePlay.Services.PlayerCargos.Storage.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Views;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerCargos.UI.Travel.Events;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;
using UniRx;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public class PlayerTravelCargoUI : MonoBehaviour, ILocalSwitchListener
    {
        [Inject]
        private void Construct(
            IInputView inputView,
            IPlayerCargoStorage storage,
            IPlayerEntityProvider entityProvider,
            IPlayerCargo cargo,
            IReputation reputation)
        {
            _cargo = cargo;
            _reputation = reputation;
            _entityProvider = entityProvider;
            _storage = storage;
            _inputView = inputView;
        }

        [SerializeField] private GameObject _body;
        [SerializeField] private CargoItemsListView _grid;
        [SerializeField] private DropConfirmation _drop;
        [SerializeField] private ShipView _shipView;
        
        private IInputView _inputView;
        private IPlayerCargoStorage _storage;
        private IPlayerEntityProvider _entityProvider;
        private IReputation _reputation;

        private IDisposable _cargoChangedListener;
        private IPlayerCargo _cargo;

        private void Awake()
        {
            _body.SetActive(false);
        }

        public void OnEnabled()
        {
            _inputView.InventoryPerformed += OnInventoryOpenPerformed;
            _cargoChangedListener = MessageBroker.Default.Receive<CargoChangedEvent>().Subscribe(OnCargoChanged);
        }

        public void OnDisabled()
        {
            _inputView.InventoryPerformed -= OnInventoryOpenPerformed;
            _cargoChangedListener.Dispose();
        }

        private void OnCargoChanged(CargoChangedEvent data)
        {
            Redraw(data.ToArray());
        }
        
        private void OnInventoryOpenPerformed()
        {
            if (_body.activeSelf == true)
                Close();
            else
                Open(_storage.ToArray());
        }

        private void Open(IItem[] items)
        {
            _shipView.Setup(_entityProvider.Resources, _reputation);
            
            _body.SetActive(true);
            _grid.Fill(items);
        }

        private void Close()
        {
            _body.SetActive(false);
        }

        private void Redraw(IItem[] items)
        {
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