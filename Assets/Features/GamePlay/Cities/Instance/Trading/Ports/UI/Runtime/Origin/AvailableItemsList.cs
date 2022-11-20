using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Items.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin
{
    public class AvailableItemsList : MonoBehaviour
    {
        [SerializeField] private AvailableItemView[] _startupCells;
        [SerializeField] private AvailableItemView _cellPrefab;
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private VerticalLayoutGroup _layout;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private ItemOrigin _origin;
        [SerializeField] private float _cellHeight = 60f;

        private readonly Dictionary<ItemType, AvailableItemView> _cells = new();
        private readonly List<AvailableItemView> _available = new();

        private IDisposable _removeListener;

        private void Awake()
        {
            foreach (var startupCell in _startupCells)
                _available.Add(startupCell);

            foreach (var cell in _available)
                cell.Disable();
        }

        private void OnEnable()
        {
            _removeListener = MessageBroker.Default.Receive<TransferCanceledEvent>().Subscribe(OnTransferCanceled);
        }

        private void OnDisable()
        {
            _removeListener?.Dispose();

            foreach (var cell in _available)
                cell.Disable();

            _cells.Clear();
        }

        public void Fill(IItem[] items, IPriceProvider priceProvider)
        {
            AddCellsOnDemand(items.Length);

            foreach (var cell in _available)
                cell.Disable();

            var unused = new List<ItemType>();

            foreach (var cell in _cells)
                unused.Add(cell.Key);

            foreach (var cell in unused)
                _cells.Remove(cell);

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];

                if (_cells.ContainsKey(item.BaseData.Type) == true)
                    continue;

                var cell = _available[i];

                cell.AssignItem(item, _origin, priceProvider);
                _cells.Add(item.BaseData.Type, cell);
            }

            CalculateVerticalSize(items.Length);
        }

        private void Update()
        {
            foreach (var cell in _cells)
                cell.Value.UpdatePrice();
        }

        private void OnTransferCanceled(TransferCanceledEvent data)
        {
            if (data.Origin != _origin)
                return;

            var cell = _cells[data.Type];

            cell.OnTransferCanceled();
        }

        private void AddCellsOnDemand(int total)
        {
            var delta = total - _available.Count;

            if (delta < 0)
                return;

            for (var i = 0; i < delta; i++)
                _available.Add(Instantiate(_cellPrefab, _cellsRoot));
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