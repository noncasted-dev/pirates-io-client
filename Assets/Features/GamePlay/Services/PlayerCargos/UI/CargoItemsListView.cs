using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.UI.Events;
using Global.Services.MessageBrokers.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI
{
    public class CargoItemsListView : MonoBehaviour
    {
        [SerializeField] private CargoItemCell _cellPrefab;

        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private VerticalLayoutGroup _layout;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private float _cellHeight = 60f;
        private readonly List<CargoItemCell> _all = new();
        private readonly List<CargoItemCell> _available = new();

        private readonly Dictionary<ItemType, CargoItemCell> _cells = new();

        private IDisposable _dropCountListener;

        private bool _isInitialized;

        private void OnEnable()
        {
            _dropCountListener = Msg.Listen<ItemDropCountChangedEvent>(OnDropCountChanged);
        }

        private void OnDisable()
        {
            _dropCountListener?.Dispose();

            foreach (var cell in _available)
                cell.Disable();
        }

        public void Fill(IReadOnlyList<IItem> items)
        {
            foreach (var cell in _all)
            {
                if (cell == null)
                    continue;

                cell.Disable();
            }

            _cells.Clear();
            _available.Clear();
            _available.AddRange(_all);

            AddCellsOnDemand(items.Count);

            foreach (var item in items)
            {
                var cell = _available[0];
                _available.RemoveAt(0);

                cell.AssignItem(item);
                _cells.Add(item.BaseData.Type, cell);
            }

            CalculateVerticalSize(items.Count);
        }

        private void OnDropCountChanged(ItemDropCountChangedEvent data)
        {
            var cell = _cells[data.Type];
            cell.OnTransferedItemCountChange(data.Count);
        }

        private void AddCellsOnDemand(int total)
        {
            var delta = total - _available.Count;

            if (delta < 0)
                return;

            for (var i = 0; i < delta; i++)
            {
                var cell = Instantiate(_cellPrefab, _cellsRoot);

                _available.Add(cell);
                _all.Add(cell);
                cell.gameObject.SetActive(false);
            }
        }

        private void CalculateVerticalSize(int itemsCount)
        {
            var cellHeight = _cellHeight;
            var spacing = _layout.spacing;

            var ySize = (itemsCount + 1) * (cellHeight + spacing);

            _contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ySize);
        }
    }
}