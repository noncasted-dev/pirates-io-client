using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeItemsList : MonoBehaviour
    {
        [SerializeField] private TradeItemView[] _startupCells;
        [SerializeField] private TradeItemView _cellPrefab;
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private VerticalLayoutGroup _layout;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private float _cellHeight = 60f;
        [SerializeField] private ItemOrigin _origin;

        private readonly Dictionary<ItemType, TradeItemView> _cells = new();
        private readonly List<TradeItemView> _available = new();

        private IDisposable _transferListener;
        private IDisposable _removeListener;
        
        private IPriceProvider _priceProvider;

        private void Awake()
        {
            foreach (var startupCell in _startupCells)
                _available.Add(startupCell);

            foreach (var cell in _available)
                cell.Disable();
        }

        private void OnEnable()
        {
            _cells.Clear();
            CalculateVerticalSize(_cells.Count);

            _transferListener = MessageBroker.Default.Receive<TransferRequestedEvent>().Subscribe(AddItem);
            _removeListener = MessageBroker.Default.Receive<TransferCanceledEvent>().Subscribe(RemoveItem);
        }

        private void OnDisable()
        {
            foreach (var cell in _cells)
            {
                cell.Value.Disable();
                _available.Add(cell.Value);
            }

            _cells.Clear();

            _transferListener?.Dispose();
            _removeListener?.Dispose();
        }

        public void Setup(IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
        }

        private void AddItem(TransferRequestedEvent data)
        {
            if (data.Origin != _origin)
                return;

            AddCellOnDemand();

            foreach (var available in _available)
                available.Disable();

            var cell = _available[0];
            cell.AssignItem(data.Tradable, data.Origin, _priceProvider);
            _available.RemoveAt(0);

            _cells.Add(data.Type, cell);

            CalculateVerticalSize(_cells.Count);
        }

        private void RemoveItem(TransferCanceledEvent data)
        {
            if (data.Origin != _origin)
                return;

            var cell = _cells[data.Type];

            _cells.Remove(data.Type);
            cell.Disable();
            _available.Add(cell);

            CalculateVerticalSize(_cells.Count);
        }

        private void AddCellOnDemand()
        {
            if (_available.Count != 0)
                return;

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