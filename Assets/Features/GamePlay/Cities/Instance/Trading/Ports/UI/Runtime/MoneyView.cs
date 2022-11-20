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

            UpdateView(_wallet.Money);

            _wallet.Changed += UpdateView;
        }

        private void OnDisable()
        {
            if (_wallet == null)
                return;

            _wallet.Changed -= UpdateView;
        }

        private void UpdateView(int count)
        {
            _text.text = count.ToString();
        }
    }
}