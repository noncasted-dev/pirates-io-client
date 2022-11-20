using GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade.Events;
using GamePlay.Services.Wallets.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    [DisallowMultipleComponent]
    public class TradeMoney : MonoBehaviour
    {
        [Inject]
        private void Construct(IWallet wallet)
        {
            _wallet = wallet;
        }
        
        [SerializeField] private Slider _countSlider;
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private TMP_Text _cost;

        private IWallet _wallet;

        private void Awake()
        {
            _countSlider.minValue = 0;
        }

        private void OnEnable()
        {
            _countSlider.value = 0f;
            _sliderValue.text = "0";
            
            _countSlider.onValueChanged.AddListener(OnSliderValueChanged);
            
            if (_wallet == null)
                return;

            _wallet.Changed += OnMoneyChanged;
            OnMoneyChanged(_wallet.Money);
        }

        private void OnDisable()
        {
            _countSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
            
                        
            if (_wallet == null)
                return;

            _wallet.Changed += OnMoneyChanged;
        }

        private void OnMoneyChanged(int count)
        {
            _countSlider.maxValue = count;
            _cost.text = count.ToString();
        }
        
        private void OnSliderValueChanged(float value)
        {
            var cost = (int)value;
            _sliderValue.text = $"{cost}";
            
            var tradeChange = new TradeMoneyAddedEvent(ItemOrigin.Cargo, cost);
            MessageBroker.Default.Publish(tradeChange);
        }
    }
}