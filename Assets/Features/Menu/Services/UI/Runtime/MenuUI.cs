using Global.Services.UiStateMachines.Runtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Menu.Services.UI.Runtime
{
    [DisallowMultipleComponent]
    public class MenuUI : MonoBehaviour, IMenuUI, IUiState
    {
        [Inject]
        private void Construct(
            IUiStateMachine uiStateMachine,
            UiConstraints constraints)
        {
            _uiStateMachine = uiStateMachine;
            _constraints = constraints;
        }

        [SerializeField] private TMP_Text _connectionErrorText;
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Button _playButton;

        [SerializeField] private GameObject _loadingBody;
        [SerializeField] private GameObject _loginBody;

        private UiConstraints _constraints;
        private IUiStateMachine _uiStateMachine;

        public UiConstraints Constraints => _constraints;
        public string Name => "MainMenu";

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
        }

        public void Recover()
        {
            _loginBody.SetActive(true);
            _loadingBody.SetActive(false);
        }

        public void Exit()
        {
            _loginBody.SetActive(false);
            _loadingBody.SetActive(false);
        }

        public void OnLogin()
        {
            _uiStateMachine.EnterAsSingle(this);

            _loginBody.SetActive(true);
            _loadingBody.SetActive(false);
        }

        public void OnLoginWithError(string error)
        {
            _loginBody.SetActive(true);
            _loadingBody.SetActive(false);

            _connectionErrorText.text = error;
        }

        public void OnLoading()
        {
            _loginBody.SetActive(false);
            _loadingBody.SetActive(true);
        }

        private void OnPlayClicked()
        {
            var userName = _nameInput.text;

            if (string.IsNullOrWhiteSpace(userName) == true)
                return;

            var clicked = new PlayClickedEvent(userName);
            MessageBroker.Default.Publish(clicked);
        }
    }
}