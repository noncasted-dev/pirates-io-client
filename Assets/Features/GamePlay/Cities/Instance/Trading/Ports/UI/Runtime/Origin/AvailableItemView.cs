using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Origin
{
    public class AvailableItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _transferButton;
        
        private TradableItem _item;
        private ItemOrigin _origin;
        public TradableItem Item => _item;

        private void OnEnable()
        {
            _transferButton.onClick.AddListener(OnTransferClicked);
            
            if (_item == null)
                return;

            OnCountChanged(_item.Item.Count);
            _item.Item.CountChanged += OnCountChanged;
        }

        private void OnDisable()
        {
            _transferButton.onClick.RemoveListener(OnTransferClicked);

            if (_item == null)
                return;

            _item.Item.CountChanged -= OnCountChanged;
        }

        public void AssignItem(TradableItem item, ItemOrigin origin)
        {
            _origin = origin;
            gameObject.SetActive(true);

            _icon.sprite = item.Item.BaseData.Icon;
            _count.text = item.Item.Count.ToString();
            _cost.text = item.Cost.ToString();
            _item = item;
        }
        
        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        public void OnTransferCanceled()
        {
            _transferButton.gameObject.SetActive(true);
        }

        private void OnTransferClicked()
        {
            _transferButton.gameObject.SetActive(false);

            var data = new TransferRequestedEvent(_item, _origin);
            MessageBroker.Default.Publish(data);
        }

        private void OnCountChanged(int count)
        {
            _count.text = $"{count}";
        }
    }
}