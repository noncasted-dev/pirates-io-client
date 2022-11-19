using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.Common.InventoryGrids
{
    public class ItemsListView : MonoBehaviour, IInventoryGrid
    {
        [SerializeField] private GridCell[] _startupCells;
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private GridSelection _selection;

        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private RectTransform _contentRect;

        private readonly Dictionary<int, GridCell> _cells = new();

        private void Awake()
        {
            for (var i = 0; i < _startupCells.Length; i++)
            {
                var cell = _startupCells[i];
                _cells.Add(i, cell);
                cell.Setup(i);
            }
        }

        public event Action Deselected;

        public event Action<IItem, GridCell> Clicked;

        public void Fill(IItem[] items)
        {
            AddCellsOnDemand(items.Length);

            foreach (var (_, cell) in _cells)
                cell.Disable();

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                _cells[i].AssignItem(item);
            }

            _selection.UpdateSelection();

            CalculateVerticalSize(items.Length);
        }

        private void AddCellsOnDemand(int total)
        {
            var delta = _cells.Count - total;

            if (delta < 0)
                AddCells(delta);
        }

        private void AddCells(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var cell = Instantiate(_cellPrefab, _cellsRoot);
                cell.Setup(_cells.Count);
            }
        }

        public void Deselect()
        {
            _selection.Disable();
        }

        private void CalculateVerticalSize(int itemsCount)
        {
            var cellSize = _gridLayout.cellSize.y;
            var spacing = _gridLayout.spacing.y;

            var ySize = (itemsCount + 1) * (cellSize + spacing);

            _contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ySize);
        }
    }
}