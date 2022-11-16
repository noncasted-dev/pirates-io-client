#region

using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.DroppedObjects.Presenter.Runtime;
using GamePlay.Services.PlayerCargos.UI.City;
using GamePlay.Services.PlayerCargos.UI.Travel;
using Global.Services.InputViews.Runtime;
using Local.Services.Abstract.Callbacks;
using UnityEngine;
using VContainer;

#endregion

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

        private PlayerCargoStorage _storage;
        private IPlayerTravelCargoUI _travelUI;
        private IPlayerCityCargoUI _cityUI;
        private IInputView _inputView;
        private IDroppedObjectsPresenter _droppedObjectsPresenter;

        private void Awake()
        {
            _storage = GetComponent<PlayerCargoStorage>();
        }

        public void OnEnabled()
        {
            _travelUI.Dropped += OnDropped;
            _inputView.InventoryPerformed += OnInventoryOpenPerformed;
        }

        public void OnDisabled()
        {
            _travelUI.Dropped -= OnDropped;
            _inputView.InventoryPerformed -= OnInventoryOpenPerformed;
        }

        public void OpenCityUI()
        {
            _travelUI.Open(_storage.ToArray());
        }

        public void OpenTravelUI()
        {
            _cityUI.Open(_storage.ToArray());
        }

        public void CloseUI()
        {
            if (_travelUI.IsActive == true)
                _travelUI.Close();

            if (_cityUI.IsActive == true)
                _cityUI.Close();
        }

        private void OnDropped(IItem item, Action<IItem[]> redrawCallback)
        {
            _droppedObjectsPresenter.DropFromPlayer(item);

            _storage.Delete(item.BaseData.Type);

            redrawCallback?.Invoke(_storage.ToArray());
        }

        private void OnInventoryOpenPerformed()
        {
            if (_travelUI.IsActive == false)
                _travelUI.Open(_storage.ToArray());
            else
                _travelUI.Close();
        }
    }
}