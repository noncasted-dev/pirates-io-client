using System;
using GamePlay.Items.Abstract;
using GamePlay.Services.Common.InventoryGrids;
using UnityEngine;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public class PlayerTravelCargoUI : MonoBehaviour, IPlayerTravelCargoUI
    {
        [SerializeField] private GameObject _body;
        [SerializeField] private InventoryGrid _grid;

        [SerializeField] private DropMenu _drop;

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void OnEnable()
        {
            _grid.Selected += OnItemSelected;
            _grid.Deselected += OnItemDeselected;
            _drop.Dropped += OnDropped;

            _drop.Disable();
        }

        private void OnDisable()
        {
            _grid.Selected -= OnItemSelected;
            _grid.Deselected -= OnItemDeselected;
            _drop.Dropped -= OnDropped;

            _drop.Disable();
        }

        public bool IsActive => _body.activeSelf;
        public event Action<IItem, int, Action<IItem[]>> Dropped;

        public Action<IItem[]> Open(IItem[] items)
        {
            _body.SetActive(true);
            _grid.Fill(items);
            _drop.Disable();

            return Redraw;
        }

        public void Close()
        {
            _body.SetActive(false);
        }

        private void OnItemSelected(IItem item)
        {
            _drop.Enable(item);
        }

        private void OnItemDeselected()
        {
            _drop.Disable();
        }

        private void Redraw(IItem[] items)
        {
            _grid.Fill(items);
        }

        private void OnDropped(IItem item, int count)
        {
            _grid.Deselect();
            Dropped?.Invoke(item, count, Redraw);
        }
    }
}