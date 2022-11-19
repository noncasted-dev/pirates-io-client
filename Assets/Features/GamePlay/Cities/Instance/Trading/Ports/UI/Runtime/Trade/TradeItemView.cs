using GamePlay.Cities.Instance.Trading.Ports.Root.Runtime;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    public class TradeItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _removeButton;
        
        private TradableItem _item;
        private ItemOrigin _origin;
        public TradableItem Item => _item;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(OnTransferClicked);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(OnTransferClicked);
        }

        public void AssignItem(TradableItem item, ItemOrigin origin)
        {
            _origin = origin;
            gameObject.SetActive(true);

            _icon.sprite = item.Item.BaseData.Icon;

            _item = item;
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        private void OnTransferClicked()
        {
            _removeButton.gameObject.SetActive(false);

            var data = new TransferCanceledEvent(_item.Item, _origin);
            MessageBroker.Default.Publish(data);
        }
    }
}