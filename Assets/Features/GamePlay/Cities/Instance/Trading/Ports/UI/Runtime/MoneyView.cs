using GamePlay.Services.Wallets.Runtime;
using TMPro;
using UnityEngine;
using VContainer;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    public class MoneyView : MonoBehaviour
    {
        [Inject]
        private void Construct(IWallet wallet)
        {
            _wallet = wallet;
        }

        [SerializeField] private TMP_Text _text;

        private IWallet _wallet;

        private void OnEnable()
        {
            if (_wallet == null)
                return;

            UpdateMoney(_wallet.Money);

            _wallet.MoneyChanged += UpdateMoney;
        }

        private void OnDisable()
        {
            if (_wallet == null)
                return;

            _wallet.MoneyChanged -= UpdateMoney;
        }

        private void UpdateMoney(int count)
        {
            _text.text = count.ToString();
        }
    }
}