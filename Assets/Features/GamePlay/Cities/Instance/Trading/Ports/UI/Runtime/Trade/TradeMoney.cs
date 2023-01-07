using TMPro;
using UnityEngine;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime.Trade
{
    [DisallowMultipleComponent]
    public class TradeMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cost;

        private void OnEnable()
        {
            _cost.text = "0";
        }

        public void SetAmount(int amount)
        {
            if (amount == 0)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);

            _cost.text = $"{amount}";
        }
    }
}