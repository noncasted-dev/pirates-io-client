using Global.Services.LoadingScreens.Logs;
using Global.Services.UiStateMachines.Runtime;
using UnityEngine;
using VContainer;

namespace Global.Services.LoadingScreens.Runtime
{
    [DisallowMultipleComponent]
    public class LoadingScreen : MonoBehaviour, ILoadingScreen, IUiState
    {
        [Inject]
        private void Construct(
            IUiStateMachine uiStateMachine,
            UiConstraints constraints,
            LoadingScreenLogger logger)
        {
            _uiStateMachine = uiStateMachine;
            _constraints = constraints;
            _logger = logger;
        }

        [SerializeField] private GameObject _canvas;
        
        private UiConstraints _constraints;
        private IUiStateMachine _uiStateMachine;

        private LoadingScreenLogger _logger;

        public UiConstraints Constraints => _constraints;
        public string Name => "LoadingScreen";

        public void Show()
        {
            _canvas.SetActive(true);

            _logger.OnShown();
            
            _uiStateMachine.EnterAsSingle(this);
        }

        public void Hide()
        {
            _canvas.SetActive(false);

            _logger.OnHidden();
            
            _uiStateMachine.Exit(this);
        }
        
        public void Recover()
        {
            Show();
        }

        public void Exit()
        {
        }
    }
}