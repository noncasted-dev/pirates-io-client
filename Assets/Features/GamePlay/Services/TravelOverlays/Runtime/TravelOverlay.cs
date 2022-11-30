using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GamePlay.Services.TravelOverlays.Runtime
{
    public class TravelOverlay : MonoBehaviour, IUiState, ITravelOverlay
    {
        [Inject]
        private void Construct(
            IUiStateMachine stateMachine,
            UiConstraints constraints)
        {
            _stateMachine = stateMachine;
            _constraints = constraints;
        }

        [SerializeField] private GameObject _body;
        [SerializeField] private GameObject _menuBody;
        [SerializeField] private Button _menuButton;
        
        private UiConstraints _constraints;
        private IUiStateMachine _stateMachine;

        public UiConstraints Constraints => _constraints;
        public string Name => "TravelOverlay";

        private void Awake()
        {
            _body.SetActive(false);
        }

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(OnMenuClicked);
        }
        
        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(OnMenuClicked);
        }

        public void Open()
        {
            _body.SetActive(true);
            _menuBody.SetActive(false);

            _stateMachine.EnterAsSingle(this);
        }

        public void Recover()
        {
            _body.SetActive(true);
        }

        public void Exit()
        {
            _body.SetActive(false);
            _menuBody.SetActive(false);
        }

        private void OnMenuClicked()
        {
            if (_menuBody.activeSelf == true)
                _menuBody.SetActive(false);
            else
                _menuBody.SetActive(true);
        }
    }
}