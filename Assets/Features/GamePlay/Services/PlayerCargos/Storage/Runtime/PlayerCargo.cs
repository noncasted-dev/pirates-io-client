using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.PlayerCargos.UI.City;
using GamePlay.Services.PlayerCargos.UI.Travel;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

namespace GamePlay.Services.PlayerCargos.Storage.Runtime
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerCargoStorage))]
    public class PlayerCargo : MonoBehaviour, IPlayerCargo, ILocalSwitchListener
    {
        [Inject]
        private void Construct(
            IPlayerTravelCargoUI travelUI,
            IPlayerCityCargoUI cityUI,
            IInputView inputView,
            IDroppedObjectsPresenter droppedObjectsPresenter)
        {
            _droppedObjectsPresenter = droppedObjectsPresenter;
            _inputView = inputView;
            _cityUI = cityUI;
            _travelUI = travelUI;
        }

        private IPlayerCityCargoUI _cityUI;

        private Action<IItem[]> _currentDrawTarget;
        private IDroppedObjectsPresenter _droppedObjectsPresenter;
        private IInputView _inputView;

        private PlayerCargoStorage _storage;
        private IPlayerTravelCargoUI _travelUI;

        private void Awake()
        {
            _storage = GetComponent<PlayerCargoStorage>();
        }

        public void OnEnabled()
        {
            _travelUI.Dropped += OnDropped;
            _inputView.InventoryPerformed += OnInventoryOpenPerformed;
            _storage.Changed += OnStorageChanged;
        }

        public void OnDisabled()
        {
            _travelUI.Dropped -= OnDropped;
            _inputView.InventoryPerformed -= OnInventoryOpenPerformed;
            _storage.Changed -= OnStorageChanged;
        }

        public void OpenCityUI()
        {
            _currentDrawTarget = _travelUI.Open(_storage.ToArray());
        }

        public void OpenTravelUI()
        {
            _currentDrawTarget = _cityUI.Open(_storage.ToArray());
        }

        public void CloseUI()
        {
            if (_travelUI.IsActive == true)
                _travelUI.Close();

            if (_cityUI.IsActive == true)
                _cityUI.Close();

            _currentDrawTarget = null;
        }

        private void OnDropped(IItem item, int count, Action<IItem[]> redrawCallback)
        {
            _droppedObjectsPresenter.DropFromPlayer(item);

            var type = item.BaseData.Type;

            _storage.Reduce(type, count);

            if (_storage.Items[type].Count == 0)
                _storage.Delete(type);

            redrawCallback?.Invoke(_storage.ToArray());
        }

        private void OnInventoryOpenPerformed()
        {
            if (_travelUI.IsActive == false)
                _currentDrawTarget = _travelUI.Open(_storage.ToArray());
            else
                _travelUI.Close();
        }

        private void OnStorageChanged()
        {
            _currentDrawTarget?.Invoke(_storage.ToArray());
        }
    }
}