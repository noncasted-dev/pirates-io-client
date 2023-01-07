using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Cities.Instance.Trading.Ports.UI.Runtime
{
    [DisallowMultipleComponent]
    public class PortListsSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _marketButton;
        [SerializeField] private Button _shipButton;

        [SerializeField] private GameObject _marketRoot;
        [SerializeField] private GameObject _shipRoot;

        private void OnEnable()
        {
            _marketButton.onClick.AddListener(OnMarketClicked);
            _shipButton.onClick.AddListener(OnShipClicked);

            _marketRoot.SetActive(true);
            _shipRoot.SetActive(false);
        }

        private void OnDisable()
        {
            _marketButton.onClick.RemoveListener(OnMarketClicked);
            _shipButton.onClick.RemoveListener(OnShipClicked);
        }

        private void OnMarketClicked()
        {
            _marketRoot.SetActive(true);
            _shipRoot.SetActive(false);
        }

        private void OnShipClicked()
        {
            _marketRoot.SetActive(false);
            _shipRoot.SetActive(true);
        }
    }
}