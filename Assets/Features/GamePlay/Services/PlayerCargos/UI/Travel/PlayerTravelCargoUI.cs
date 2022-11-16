#region

using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.Common.InventoryGrids;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public class PlayerTravelCargoUI : MonoBehaviour, IPlayerTravelCargoUI
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private InventoryGrid _grid;
        [SerializeField] private Button _dropButton;

        private IItem _selected;

        public bool IsActive => _body.activeSelf;
        public event Action<IItem, Action<IItem[]>> Dropped;

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void OnEnable()
        {
            _grid.Selected += OnItemSelected;
            _grid.Deselected += OnItemDeselected;

            _dropButton.onClick.AddListener(OnDropClicked);
        }

        private void OnDisable()
        {
            _grid.Selected -= OnItemSelected;
            _grid.Deselected -= OnItemDeselected;

            _dropButton.onClick.RemoveListener(OnDropClicked);
        }

        public void Open(IItem[] items)
        {
            _body.SetActive(true);
            _grid.Fill(items);
            _dropButton.gameObject.SetActive(false);
        }

        public void Close()
        {
            _body.SetActive(false);
        }

        private void OnItemSelected(IItem item)
        {
            _selected = item;
            _dropButton.gameObject.SetActive(true);
        }

        private void OnItemDeselected()
        {
            _selected = null;
            _dropButton.gameObject.SetActive(false);
        }

        private void OnDropClicked()
        {
            if (_selected == null)
            {
                Debug.LogError("No item to drop selected");
                return;
            }

            Dropped?.Invoke(_selected, Redraw);
        }

        private void Redraw(IItem[] items)
        {
            _grid.Fill(items);
        }
    }
}