using System;
using System.Collections.Generic;
using GamePlay.Cities.Instance.Storage.Runtime;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin.Events;
using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Items.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin
{
    public class StoredItemsList : MonoBehaviour
    {
        [SerializeField] private StoredItemView[] _startupCells;
        [SerializeField] private StoredItemView _cellPrefab;
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private VerticalLayoutGroup _layout;
        [SerializeField] private RectTransform _contentRect;
        [SerializeField] private ItemOrigin _origin;
        [SerializeField] private float _cellHeight = 60f;

        private readonly Dictionary<ItemType, StoredItemView> _cells = new();
        private readonly List<StoredItemView> _available = new();

        private IDisposable _removeListener;
        private IDisposable _tradeAddListener;

        private void Awake()
        {
            foreach (var startupCell in _startupCells)
                _available.Add(startupCell);

            _startupCells = Array.Empty<StoredItemView>();

            foreach (var cell in _available)
                cell.Disable();
        }

        private void OnEnable()
        {
            _removeListener = MessageBroker.Default.Receive<TransferCanceledEvent>().Subscribe(OnTransferCanceled);
            _tradeAddListener = MessageBroker.Default.Receive<TradeAddedEvent>().Subscribe(OnTradeAdd);
        }

        private void OnDisable()
        {
            _removeListener?.Dispose();
            _tradeAddListener?.Dispose();

            foreach (var cell in _available)
                cell.Disable();
        }

        public void Fill(IReadOnlyList<IItem> items, IPriceProvider priceProvider)
        {
            Awake();
            
            foreach (var cell in _cells)
                _available.Add(cell.Value);
            
            foreach (var cell in _available)
                cell.Disable();

            _cells.Clear();

            AddCellsOnDemand(items.Count);

            foreach (var item in items)
            {
                var cell = _available[0];
                _available.RemoveAt(0);

                cell.AssignItem(item, _origin, priceProvider);
                _cells.Add(item.BaseData.Type, cell);
            }

            CalculateVerticalSize(items.Count);
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
        
        private void OnTradeAdd(TradeAddedEvent data)
        {
            if (data.Origin != _origin)
                return;

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