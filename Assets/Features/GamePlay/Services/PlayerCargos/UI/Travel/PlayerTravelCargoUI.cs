using System;
using Cysharp.Threading.Tasks;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.Storage.Runtime;
using GamePlay.Services.PlayerCargos.UI.Travel.Events;
using GamePlay.Services.PlayerPositionProviders.Runtime;
using GamePlay.Services.Reputation.Runtime;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;
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
            IReputation reputation)
        {
            _reputation = reputation;
            _entityProvider = entityProvider;
            _storage = storage;
            _inputView = inputView;
        }

        [SerializeField] private GameObject _body;
        [SerializeField] private CargoItemsListView _grid;
        [SerializeField] private DropConfirmation _drop;

        private IInputView _inputView;
        private IPlayerCargoStorage _storage;
        private IPlayerEntityProvider _entityProvider;
        private IReputation _reputation;

        private void Awake()
        {
            _body.SetActive(false);
        }

        public bool IsActive => _body.activeSelf;

        public void OnEnabled()
        {
            _inputView.InventoryPerformed += OnInventoryOpenPerformed;
            _storage.Changed += OnStorageChanged;
        }

        public void OnDisabled()
        {
            _inputView.InventoryPerformed -= OnInventoryOpenPerformed;
            _storage.Changed -= OnStorageChanged;
        }

        private void OnStorageChanged()
        {
            Redraw(_storage.ToArray());
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

        private void OnDropped(IItem item, int count)
        {
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
                    _storage.Reduce(dropRequest.Type, result.Count);
                    break;
                case DropConfirmationResultType.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}