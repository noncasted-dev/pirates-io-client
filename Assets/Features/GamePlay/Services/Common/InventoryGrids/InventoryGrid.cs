#region

using System;
using System.Collections.Generic;
using GamePlay.Items.Abstract;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace GamePlay.Services.Common.InventoryGrids
{
    public class InventoryGrid : MonoBehaviour, IInventoryGrid
    {
        [SerializeField] private GridCell[] _startupCells;
        [SerializeField] private GridCell _cellPrefab;
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private GridSelection _selection;

        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private int _cellsInRow = 4;

        private readonly Dictionary<int, GridCell> _cells = new();

        public event Action<IItem> Selected;
        public event Action Deselected;

        private void Awake()
        {
            for (var i = 0; i < _startupCells.Length; i++)
            {
                var cell = _startupCells[i];
                _cells.Add(i, cell);
                cell.Setup(i);
            }
        }

        private void OnEnable()
        {
            foreach (var cell in _cells)
                cell.Value.Selected += OnSelected;
        }

        private void OnDisable()
        {
            foreach (var cell in _cells)
                cell.Value.Selected -= OnSelected;
        }

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
                cell.Selected += OnSelected;
            }
        }

        private void OnSelected(GridCell cell)
        {
            if (_selection.IsSelected(cell) == true)
            {
                _selection.Disable();
                Deselected?.Invoke();
                return;
            }

            _selection.Move(cell);

            Selected?.Invoke(cell.Item);
        }

        private void CalculateVerticalSize(int itemsCount)
        {
            var cellSize = _gridLayout.cellSize.y;
            var spacing = _gridLayout.spacing.y;

            var rows = itemsCount / _cellsInRow;

            var ySize = (rows + 1) * (cellSize + spacing);

            _contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ySize);
        }
    }
}