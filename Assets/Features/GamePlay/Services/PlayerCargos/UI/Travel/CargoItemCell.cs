using GamePlay.Items.Abstract;
using GamePlay.Services.PlayerCargos.UI.Travel.Events;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Services.PlayerCargos.UI.Travel
{
    public class CargoItemCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Button _transferButton;

        private IItem _item;

        private void OnEnable()
        {
            _transferButton.onClick.AddListener(OnTransferClicked);
            
            if (_item == null)
                return;

            _item.CountChanged += OnCountChanged;
        }

        private void OnDisable()
        {
            _transferButton.onClick.RemoveListener(OnTransferClicked);

            if (_item == null)
                return;

            _item.CountChanged -= OnCountChanged;
        }

        public void AssignItem(IItem item)
        {
            _item = item;
            
            gameObject.SetActive(true);
            _transferButton.gameObject.SetActive(true);

            _icon.sprite = item.BaseData.Icon;
            _count.text = item.Count.ToString();

            OnCountChanged(_item.Count);
        }

        public void OnTransferedItemCountChange(int count)
        {
            var delta = _item.Count - count;

            _count.text = delta.ToString();

            if (delta == 0)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }

        public void Disable()
        {
            _item = null;
            gameObject.SetActive(false);
        }

        public void OnTransferCanceled()
        {
            gameObject.SetActive(true);
            _count.text = _item.Count.ToString();

            _transferButton.gameObject.SetActive(true);
        }

        private void OnTransferClicked()
        {
            _transferButton.gameObject.SetActive(false);

            var data = new ItemDropRequestedEvent(_item);

            MessageBroker.Default.Publish(data);
        }

        private void OnCountChanged(int count)
        {
            _count.text = count.ToString();
        }
    }
}