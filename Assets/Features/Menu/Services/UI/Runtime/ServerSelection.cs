using System;
using Global.Services.Network.Connection.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Services.UI.Runtime
{
    [DisallowMultipleComponent]
    public class ServerSelection : MonoBehaviour
    {
        [SerializeField] private Button _euButton;
        [SerializeField] private Button _usaButton;

        [SerializeField] private GameObject _euNotSelected;
        [SerializeField] private GameObject _usaNotSelected;
        
        [SerializeField] private GameObject _euSelected;
        [SerializeField] private GameObject _usaSelected;
        
        private TargetServer _selected;

        public TargetServer Selected => _selected;

        private void Awake()
        {
            _selected = TargetServer.Europe;

            _euSelected.SetActive(true);
            _euNotSelected.SetActive(false);

            _usaSelected.SetActive(false);
            _usaNotSelected.SetActive(true);
        }

        private void OnEnable()
        {
            _euButton.onClick.AddListener(OnEuropeClicked);
            _usaButton.onClick.AddListener(OnUsaClicked);
        }
        
        private void OnDisable()
        {
            _euButton.onClick.RemoveListener(OnEuropeClicked);
            _usaButton.onClick.RemoveListener(OnUsaClicked);
        }

        private void OnEuropeClicked()
        {
            _selected = TargetServer.Europe;

            _euSelected.SetActive(true);
            _euNotSelected.SetActive(false);

            _usaSelected.SetActive(false);
            _usaNotSelected.SetActive(true);
        }

        private void OnUsaClicked()
        {
            _selected = TargetServer.USA;
            
            _euSelected.SetActive(false);
            _euNotSelected.SetActive(true);

            _usaSelected.SetActive(true);
            _usaNotSelected.SetActive(false);
        }
    }
}