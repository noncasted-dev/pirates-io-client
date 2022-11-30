using Global.Services.Network.Connection.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Services.UI.Runtime
{
    [DisallowMultipleComponent]
    public class ServerSelection : MonoBehaviour
    {
        [SerializeField] private Button _euButton;
        [SerializeField] private Button _sanFranciscoButton;
        [SerializeField] private Button _newYorkButton;

        [SerializeField] private GameObject _euNotSelected;
        [SerializeField] private GameObject _sanFranciscoNotSelected;
        [SerializeField] private GameObject _newYorkNotSelected;
        
        [SerializeField] private GameObject _euSelected;
        [SerializeField] private GameObject _sanFranciscoSelected;
        [SerializeField] private GameObject _newYorkSelected;
        
        private TargetServer _selected;

        public TargetServer Selected => _selected;

        private void Awake()
        {
            OnEuropeClicked();
        }

        private void OnEnable()
        {
            _euButton.onClick.AddListener(OnEuropeClicked);
            _sanFranciscoButton.onClick.AddListener(OnSanFranciscoClicked);
            _newYorkButton.onClick.AddListener(OnNewYorkClicked);
        }
        
        private void OnDisable()
        {
            _euButton.onClick.RemoveListener(OnEuropeClicked);
            _sanFranciscoButton.onClick.RemoveListener(OnSanFranciscoClicked);
            _newYorkButton.onClick.RemoveListener(OnNewYorkClicked);
        }

        private void OnEuropeClicked()
        {
            _selected = TargetServer.Europe;
            
            DisableAll();

            _euNotSelected.SetActive(false);
            _euSelected.SetActive(true);
        }

        private void OnSanFranciscoClicked()
        {
            _selected = TargetServer.USA_SanFrancisco;
            
            DisableAll();

            _sanFranciscoNotSelected.SetActive(false);
            _sanFranciscoSelected.SetActive(true);
        }

        private void OnNewYorkClicked()
        {
            _selected = TargetServer.USA_NewYork;
            
            DisableAll();

            _newYorkNotSelected.SetActive(false);
            _newYorkSelected.SetActive(true);
        }

        private void DisableAll()
        {
            _euSelected.SetActive(false);
            _sanFranciscoSelected.SetActive(false);
            _newYorkSelected.SetActive(false);
            
            _euNotSelected.SetActive(true);
            _sanFranciscoNotSelected.SetActive(true);
            _newYorkNotSelected.SetActive(true);
        }
    }
}