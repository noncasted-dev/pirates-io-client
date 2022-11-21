using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events
{
    [DisallowMultipleComponent]
    public class TradeApplyButton : MonoBehaviour
    {
        [SerializeField] private Sprite _available;
        [SerializeField] private Sprite _unavailable;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public event Action Clicked;
        
        private void OnEnable()
        {
            OnUnavailable();
            
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void OnAvailable()
        {
            _image.sprite = _available;
            _button.interactable = true;
        }

        public void OnUnavailable()
        {
            _image.sprite = _unavailable;
            _button.interactable = false;
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}